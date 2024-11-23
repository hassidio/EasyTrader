using EasyTrader.Services.DataServices;
using EasyTrader.UserControls.MarketList;
using EasyTrader.Forms;

namespace EasyTrader.UserControls.Collections
{
    public partial class MarketListCollectionUserControl : CommonUserControl
    {
        public MarketListCollectionUserControl()
        {
            InitializeComponent();

            InitializeCommonComponent();
        }

        public Label Title { get { return labelTitle; } }

        public int NumberOfPageItems { get; set; }

        public ICollection<string> CollectionData { get; set; }

        public IList<MarketListItemUserControl> CollectionPageUserControls { get; private set; }

        public ExchangeDataManager ExchangeDataManager { get; set; }

        public event EventHandler<CollectionItemSelectionChangedEventArgs> SelectionChanged;
        public event EventHandler CollectionPageChanged;

        public void LoadCollectionData(ExchangeDataManager exchangeDataManager, ICollection<string> collection)
        {
            CollectionData = collection;

            ExchangeDataManager = exchangeDataManager;
            // Load Pager
            pagerUserControl1.Load(collection.Cast<dynamic>().ToList(), NumberOfPageItems);

            //SetPagedControls();
        }

        public Action GetOpenItemAction(string marketName)
        {
            return new Action(() =>
            {
                var market = ExchangeDataManager.GetMarket("ETHBTC");
                if (market is not null)
                {
                    var form = new InformationForm();
                    form.LoadData(market, $"{market.ExchangeName} ({market.EntityId})" );
                    form.Show();
                }
                else
                {
                    MessageBox.Show($"The market [{marketName}] data is not found.");
                }
            });
        }

        public Action GetUpdateItemAction(string data)
        {
            return new Action(() => MessageBox.Show($"todo Update Action: {data}"));
        }

        private void SetPagedControls()
        {
            Cursor.Current = Cursors.WaitCursor;

            ClearTableLayoutPanelThreadSafe(tableLayoutPanelContent);
            CollectionPageUserControls = new List<MarketListItemUserControl>();

            var count = 0;
            var pageItems = pagerUserControl1.Pager.PageItems.Cast<string>().ToList();
            foreach (var item in pageItems)
            {
                var marketListItemUserControl = new MarketListItemUserControl();
                marketListItemUserControl.ItemSelected += MarketListItemUserControl_ItemSelected;

                marketListItemUserControl.LoadCollectionData(
                    data: item,
                    index: count,
                    updateAction: GetUpdateItemAction(item),
                    openAction: GetOpenItemAction(item));

                CollectionPageUserControls.Add(marketListItemUserControl);
                AddRowTableLayoutPanelThreadSafe(tableLayoutPanelContent, marketListItemUserControl);

                count++;
            }

            Cursor.Current = Cursors.Default;
        }

        private void PagerUserControl_PageChanged(object? sender, EventArgs e)
        {
            CollectionPageChanged?.Invoke(sender, e);
        }

        private void MarketListCollectionUserControl_CollectionPageChanged(object? sender, EventArgs e)
        {
            SetPagedControls();
        }

        private void MarketListItemUserControl_ItemSelected(object? sender, CollectionItemSelectionChangedEventArgs e)
        {
            foreach (var control in CollectionPageUserControls)
            {
                control.ClearSelection();
            }

            SelectionChanged?.Invoke(this, e);
        }




        private void FilterCollectionClear()
        {
            textBoxFilter.Text = string.Empty;
            FilterCollection();
        }

        private void FilterCollection()
        {
            if (CollectionPageUserControls is null) { return; }

            var textFilter = textBoxFilter.Text.ToLower();

            if (string.IsNullOrEmpty(textFilter))
            {
                pagerUserControl1.Load(CollectionData.Cast<dynamic>().ToList(), NumberOfPageItems);
                SetPagedControls();
                return;
            }

            ICollection<string> filteredData = CollectionData.Where(c => (c.ToLower().Contains(textFilter))).ToHashSet();
            pagerUserControl1.Load(filteredData.Cast<dynamic>().ToList(), NumberOfPageItems);
            SetPagedControls();
        }

        private void InitializeCommonComponent()
        {
            pagerUserControl1.Pager.PageChanged += PagerUserControl_PageChanged;

            CollectionPageChanged += MarketListCollectionUserControl_CollectionPageChanged;

            actionUserControlFilterSearch.Load(new Action(() => FilterCollection()));
            actionUserControlFilterClear.Load(new Action(() => FilterCollectionClear()));
        }
    }
}
