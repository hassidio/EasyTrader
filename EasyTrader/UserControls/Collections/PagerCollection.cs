
namespace EasyTrader.UserControls.Collections
{
    public class PagerCollection
    {
        public PagerCollection(
            PagerUserControl pagerUserControl, 
            int numberOfPageItems)
        {
            _pagerUserControl = pagerUserControl;
            _numberOfPageItems = numberOfPageItems;

            _pagerUserControl.Pager.PageChanged += PagerUserControl_PageChanged;

            CollectionPageChanged += PagerCollection_CollectionPageChanged;
        }


        private PagerUserControl _pagerUserControl;
        public int _numberOfPageItems;

        public IList<dynamic> PageItems { get { return _pagerUserControl.Pager.PageItems; } }

        public ICollection<dynamic> CollectionData { get; set; }

        public IList<PropertyDataUserControl> CollectionPageUserControls { get; private set; }


        public event EventHandler<CollectionItemSelectionChangedEventArgs> SelectionChanged;
        public event EventHandler CollectionPageChanged;

        public void LoadCollectionData(ICollection<dynamic> collection)
        {

            CollectionData = collection;

            // Load Pager
            _pagerUserControl.Load(collection, _numberOfPageItems);

            SetPagedControls();

        }

        private void SetPagedControls()
        {
            Cursor.Current = Cursors.WaitCursor;

            CollectionPageUserControls = new List<PropertyDataUserControl>();

            var count = 0;
            var pageItems = _pagerUserControl.Pager.PageItems;
            foreach (var item in pageItems)
            {
                var propertyDataUserControl = new PropertyDataUserControl();
                propertyDataUserControl.ItemSelected += PropertyDataUserControl_ItemSelected;
                propertyDataUserControl.LoadCollectionData(item, count);
                CollectionPageUserControls.Add(propertyDataUserControl);
                count++;
            }

            Cursor.Current = Cursors.Default;
        }

        private void PropertyDataUserControl_ItemSelected(object? sender, CollectionItemSelectionChangedEventArgs e)
        {
            foreach (var control in CollectionPageUserControls)
            {
                control.ClearSelection();
            }

            SelectionChanged?.Invoke(this, e);
        }

        private void PagerUserControl_PageChanged(object? sender, EventArgs e)
        {
            CollectionPageChanged?.Invoke(sender, e);
        }


        private void PagerCollection_CollectionPageChanged(object? sender, EventArgs e)
        {
            SetPagedControls();
        }



    }

}
