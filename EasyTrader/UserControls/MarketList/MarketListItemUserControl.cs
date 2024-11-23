using EasyTrader.Forms;
using EasyTrader.UserControls.Collections;


namespace EasyTrader.UserControls.MarketList
{
    public partial class MarketListItemUserControl : CommonUserControl, ICollectionItemUserControl
    {
        public MarketListItemUserControl()
        {
            InitializeComponent();

            InitializeCommonComponent();
        }

        public bool IsSelected { get; set; }
        public int Index { get; set; }

        public event EventHandler<CollectionItemSelectionChangedEventArgs> ItemSelected;

        public dynamic Data { get { return labelName.Text; } }

        private void InitializeCommonComponent()
        {

        }

        public void LoadCollectionData(dynamic data, int index, Action updateAction, Action openAction)
        {
            LoadCollectionData(data, index);

            updateActionUserControl1.Load(updateAction);
            openPropertyButton1.Load(openAction);
        }

        public void LoadCollectionData(dynamic data, int index)
        {
            Name = "MarketListItem" + data;
            Index = index;
            labelName.Text = data;
            Dock = DockStyle.Top;
        }

        public void ClearSelection()
        {
            labelName.BackColor = Color.Transparent;
            IsSelected = false;
        }

        private void labelName_Click(object sender, EventArgs e)
        {
            var agrs = new CollectionItemSelectionChangedEventArgs(this, Index, true);
            OnSelected(agrs);

            IsSelected = true;
            labelName.BackColor = Constants.SelectColor;
        }

        protected virtual void OnSelected(CollectionItemSelectionChangedEventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }

        private void labelName_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected) { return; }
            labelName.BackColor = Color.Transparent;
        }

        private void labelName_MouseEnter(object sender, EventArgs e)
        {
            if (IsSelected) { return; }
            labelName.BackColor = Constants.MouseHoverColor;
        }

    }
}
