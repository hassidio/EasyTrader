using EasyTrader.Core.Common.Lists;

namespace EasyTrader.Core.Views.OutputView.OutputWindowView
{
    public class OutputWindow : CommonList<OutputWindowItem>
    {
        public OutputWindow(IClientOutputWindow clientOutputWindow)
        {
            ClientOutputWindow = clientOutputWindow;
        }

        public IClientOutputWindow ClientOutputWindow { get; set; }

        public string NextId { get { return (Count + 1).ToString(); } }

        public OutputWindowItem? GetOutputItemById(string id)
        {
            return this.Where(e => e.Id == id).FirstOrDefault();
        }

        public OutputWindowItem WriteOutputWindowItem(
            OutputWindowItem? outputItem = null,
            string? text = null,
            DateTime? dateTime = null,
            OutputItemStatusEnum? status = null) //*/ test
        {
            if (outputItem is null) { outputItem = new OutputWindowItem(); }
            if (text != null) { outputItem.Text = text; }
            if (dateTime != null) { outputItem.DateTime = (DateTime)dateTime; }
            if (status != null) { outputItem.Status = (OutputItemStatusEnum)status; }

            Add(outputItem);
            return outputItem;
        }

        public override bool OnBeforeAdd(OutputWindowItem outputItem)
        {
            //Update this list with exsiting id or create new
            var existingItem = GetOutputItemById(outputItem.Id);

            if (existingItem is not null)
            {
                existingItem.DateTime = outputItem.DateTime;
                existingItem.Status = outputItem.Status;
                existingItem.Text = outputItem.Text;

                return false;
                // don't add to base list and continue execute OnAfterAdd...
            }
            else
            {
                if (string.IsNullOrEmpty(outputItem.Id))
                { outputItem.Id = NextId; }

                return true;
                // add to base list and continue execute OnAfterAdd...
            }
        }

        public override void OnAfterAdd(OutputWindowItem item, bool isItemAdded)
        {
            //Send update to the client
            if (ClientOutputWindow is not null)
            {
                ClientOutputWindow.UpdateOutputWindow(this);
            }
        }

        public override void OnClear() { ClientOutputWindow.UpdateOutputWindow(this); }
    }
}
