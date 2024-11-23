using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.UserControls;

namespace EasyTrader
{
    public partial class OutputItemUserControl : CommonUserControl
    {
        public OutputItemUserControl()
        {
            InitializeComponent();

            InitializeCommonComponent();
        }

        public void Load(OutputWindowItem outputItem)
        {
            labelDateTime.Text =
                $"{outputItem.DateTime.ToString("dd/MM/yyyy hh:mm:ss.fff tt")} [id: {outputItem.Id}]";
            pictureBoxStatus.Image = GetStatusImage(outputItem.Status);
            labelText.Text = outputItem.Text;

            labelText.Height = GetControlTextHeight(labelText);
        }

        private Bitmap GetStatusImage(OutputItemStatusEnum status)
        {
            if (status == OutputItemStatusEnum.Success)
            { return Properties.Resources.check; }

            if (status == OutputItemStatusEnum.Fail)
            { return Properties.Resources.StatusRed; }

            return Properties.Resources.InProgress1;
        }

        public void InitializeCommonComponent()
        {

        }

        private void OutputItemUserControl_ClientSizeChanged(object sender, EventArgs e)
        {
            TableLayoutPanelCellPosition pos = tableLayoutPanelMain.GetCellPosition(labelText);
            int width = tableLayoutPanelMain.GetColumnWidths()[pos.Column];
            int height = tableLayoutPanelMain.GetRowHeights()[pos.Row];
            labelText.Height = GetControlTextHeight(labelText);
        }
    }
}
