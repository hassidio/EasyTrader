using EasyTrader.Core.Views.PropertyView;
using Newtonsoft.Json;
using System.Data;

namespace EasyTrader.Core.Models.Entities
{
    public abstract class Market : Entity, IMarket
    {
        public Market()
        {
            LeadingMarketRecommendations = new HashSet<Recommendation>();
            TrailingMarketRecommendations = new HashSet<Recommendation>();
        }

        private dynamic _candles;
        private string? _entityStamp;

        [PropertyData("Entity Id")]
        public override string EntityId { get; set; }

        [JsonIgnore]
        public override string? EntityParentId
        {
            get { return ExchangeName; }
            set { ExchangeName = value; }
        }

        [PropertyData("Exchange Name")]
        public string ExchangeName { get; set; }


        [JsonIgnore]
        [PropertyData("Exchange")]
        public abstract IExchange Exchange { get; }


        [PropertyData("Market Information")]
        public MarketInfo MarketInfo { get; set; }


        [PropertyData("Raw Data")]
        public override dynamic RawClientData { get; set; }

        [JsonIgnore]
        public override bool IsRawClientDataTypeIEnumerable { get { return true; } }


        [JsonIgnore]
        public override string? EntityStamp { get { return _entityStamp; } }


        public ICollection<Recommendation> LeadingMarketRecommendations { get; set; }
        public ICollection<Recommendation> TrailingMarketRecommendations { get; set; }


        public override void SetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? rawClientData = null)
        {
            if (!string.IsNullOrEmpty(id)) { EntityId = id; }

            if (rawClientData is not null) { RawClientData = rawClientData; }

            // Set the exchange here
            OnSetEntity(id, parentId, rawClientData);

            if (!string.IsNullOrEmpty(id) && Exchange is not null) { MarketInfo = Exchange.GetMarketInfoById(id); }
        }

        protected List<List<TCandle>> GetClientCandlesSets<TCandle>(int recodesCount)
            where TCandle : ICandle, new()
        {
            if (ClientDataList is null) { return null; }

            var setsClientCandle = new List<List<TCandle>>();
            var listClientCandle = new List<TCandle>();

            var allCandels = GetCandles<TCandle>().OrderBy(c => c.OpenDateTime).ToList();

            var count = 0;
            foreach (var candle in allCandels)
            {
                listClientCandle.Add(candle);
                count++;

                if (count >= recodesCount)
                {
                    setsClientCandle.Add(listClientCandle);
                    listClientCandle = new List<TCandle>();
                    count = 0;
                }
            }

            if (listClientCandle.Count > 0)
            { setsClientCandle.Add(listClientCandle); }

            return setsClientCandle;
        }

        public IList<TCandle> GetCandles<TCandle>() where TCandle : ICandle, new()
        {
            if (_candles is null) { RefreshCandles<TCandle>(); }
            return _candles;
        }

        [PropertyData("Candles")]
        public IList<ICandle> Candles
        {
            get
            {
                if(_candles is null) { return default; }

                var candles = new List<ICandle>();

                foreach (var candle in _candles) { candles.Add(candle); }
                return candles;
            }
        }

        public void RefreshCandles<TCandle>() where TCandle : ICandle, new()
        {
            var candles = new List<TCandle>();

            foreach (dynamic c in ClientDataList)
            {
                var candle = new TCandle();
                candle.SetCandle(EntityId, c);
                candles.Add(candle);
            }

            _candles = candles.OrderBy(c => c.OpenDateTime).ToList();

            SetEntityStamp(_candles);
        }

        private void SetEntityStamp<TCandle>(List<TCandle> candles) where TCandle : ICandle
        {
            var first = candles.First().OpenDateTime.Ticks.ToString();
            var last = candles.Last().OpenDateTime.Ticks.ToString();

            _entityStamp = $"{first}_{last}";
        }

        public void AppendClientCandlesData<TMarket, TCandle>(TMarket newMarket)
            where TMarket : IMarket, new()
            where TCandle : ICandle, new()
        {
            if (newMarket is null) { return; }
            if (newMarket.RawClientData is null) { return; }

            if (RawClientData is null)
            {
                RawClientData = newMarket.RawClientData;
            }
            else
            {
                // Get last OpenDateTime in market file
                var fileLastCandle = GetCandles<TCandle>().Last();

                // Append
                foreach (var newCandle in newMarket.GetCandles<TCandle>().Where(
                    c => c.OpenDateTime > fileLastCandle.OpenDateTime))
                {
                    AddCandle(newCandle);
                }
            }
            RefreshCandles<TCandle>();
        }

        public void AddCandle<TCandle>(TCandle candle)
            where TCandle : ICandle, new()
        {
            OnAddCandle(candle);
            RefreshCandles<TCandle>();
        }

        public abstract void OnAddCandle<TCandle>(TCandle candle)
            where TCandle : ICandle, new();


    }
}
