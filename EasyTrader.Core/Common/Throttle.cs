using EasyTrader.Core.Configuration;
using EasyTrader.Core.Views.PropertyView;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace EasyTrader.Core.Common
{
    public class Throttle
    {
        public Throttle(ApiConfiguration? apiConfiguration = null)
        {
            if(apiConfiguration is not null) { ValidateExchangeConfiguration(apiConfiguration); }

            Release();
        }

        [PropertyData("Last Call")]
        public DateTime LastCall { get; private set; }

        [PropertyData("Curren weight used")]
        public int CurrentWeightUsed { get; private set; }

        [PropertyData("Maximum total weight")]
        public int? MaximumTotalWeight { get; private set; }

        [PropertyData("Calls per second policy")]
        public int? CallsPerSecondPolicy { get; private set; }

        [PropertyData("Weight cooldown seconds policy")]
        public int? WeightCooldownSecondsPolicy { get; private set; }

        [PropertyData("Maximum message weight (so far)")]
        public int MaximumMessageWeightSoFar { get; private set; }

        public int CallEveryMilliseconds =>
            CallsPerSecondPolicy is not null ? 1000 / (int)CallsPerSecondPolicy : 0;

        /// <summary>
        /// If true throttle is idle, if false throttle is in sleep, if null throttle is not set.
        /// </summary>
        [PropertyData("Is Released")]
        public bool? IsReleased { get; private set; }

        public void AddWeight(int weight)
        {
            if (weight >= MaximumMessageWeightSoFar) { MaximumMessageWeightSoFar = weight; }
            CurrentWeightUsed += weight;
        }

        public void SetCurrentWeightUsed(int clientWeight)
        {
            Debug.WriteLine(
                $"{TimeStamp} Throttle clientWeight: {clientWeight}, ResponseCurrentWeightUsed: {CurrentWeightUsed}, MaximumMessageWeightSoFar: {MaximumMessageWeightSoFar}");

            if (clientWeight - CurrentWeightUsed >= MaximumMessageWeightSoFar)
            { MaximumMessageWeightSoFar = clientWeight - CurrentWeightUsed; }

            CurrentWeightUsed = clientWeight;
        }

        public void Release()
        {
            LastCall = DateTime.Now;
            IsReleased = true;
        }

        public void Start()
        {
            if (IsReleased is null) { return; }

            IsReleased = false;

            ThrottleWeight();

            ThrottleCallsPerSecond();

            Release();
        }

        private void ThrottleCallsPerSecond() 
        {
            if (CallEveryMilliseconds <= 0) { return; }

            double deltaMilliseconds = DateTime.Now.Subtract(LastCall).TotalMilliseconds;
            int sleep = Convert.ToInt32(CallEveryMilliseconds - deltaMilliseconds);

            if (sleep > 0) { Debug.WriteLine($"{TimeStamp}: Throttle ThrottleCallsPerSecond sleeping for {sleep} milliseconds..."); }

            while (
                sleep > 0
                && (bool)!IsReleased)
            {
                Thread.Sleep(1);

                deltaMilliseconds = DateTime.Now.Subtract(LastCall).TotalMilliseconds;
                sleep = Convert.ToInt32(CallEveryMilliseconds - deltaMilliseconds);
            }
        }

        private void ThrottleWeight() 
        {
            if (MaximumTotalWeight is null
                || WeightCooldownSecondsPolicy is null) { return; }

            var weight = MaximumTotalWeight - CurrentWeightUsed;

            if (weight < MaximumMessageWeightSoFar)
            {
                var startSleep = DateTime.Now;

                Debug.WriteLine(
                    $"{TimeStamp}: Throttle ThrottleWeight sleeping for {(int)WeightCooldownSecondsPolicy} seconds..."); ;

                while (
                    DateTime.Now < startSleep.AddSeconds((int)WeightCooldownSecondsPolicy)
                    || (bool)IsReleased)
                {
                    Thread.Sleep(1);
                }

                if ((bool)IsReleased) { return; }

                CurrentWeightUsed = 0;
            }
        }

        private void ValidateExchangeConfiguration(ApiConfiguration apiConfiguration)
        {
            CallsPerSecondPolicy =
                ValidateConfiguration(apiConfiguration.CallsPerSecondPolicy, "CallsPerSecondPolicy");

            WeightCooldownSecondsPolicy =
                ValidateConfiguration(apiConfiguration.WeightCooldownSecondsPolicy, "WeightCooldownSecondsPolicy");

            MaximumTotalWeight =
                ValidateConfiguration(apiConfiguration.MaximumTotalWeight, "MaximumTotalWeight");
        }

        private int? ValidateConfiguration(int? field, string fieldName)
        {
            if (field is null) { return field; }

            if (field > 0) { return field; }

            throw CommonException.ThrottleConfigurationError($"{fieldName}: {field}");
        }

        [JsonIgnore]
        private string TimeStamp { get { return $"{DateTime.Now.ToString("mm:ss.fff")} Thread[{Thread.CurrentThread.ManagedThreadId}]"; } }

    }
}
