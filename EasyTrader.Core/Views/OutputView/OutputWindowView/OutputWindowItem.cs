namespace EasyTrader.Core.Views.OutputView.OutputWindowView
{
    public class OutputWindowItem
    {
        public OutputWindowItem()
        {
            DateTime = DateTime.Now;
            Status = OutputItemStatusEnum.InProgress;
        }


        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public OutputItemStatusEnum Status { get; set; }
        public string Text { get; set; }


    }

    public enum OutputItemStatusEnum { InProgress, Success, Fail }



}
