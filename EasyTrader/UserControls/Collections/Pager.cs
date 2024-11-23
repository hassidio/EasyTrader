
namespace EasyTrader.UserControls.Collections
{
    public class Pager
    {
        public Pager()
        {
        }

        public int NumberOfItems { get; private set; }

        //public IList<ICollectionItemUserControl> AllItems { get; set; }
        public IList<dynamic> AllItems { get; set; }

        //public IList<ICollectionItemUserControl> PageItems { get { return GetPageItems(); } }
        public IList<dynamic> PageItems { get { return GetPageItems(); } }

        public int CurrentIndex { get; set; }
        public int LastPageIndex { get { return GetPageLastIndex(); } }

        public bool CanGoForward { get { return AllItems is not null && LastPageIndex < AllItems.Count; } }
        public bool CanGoBackward { get { return CurrentIndex > 0; } }

        public event EventHandler PageChanged;

        private int GetPageLastIndex()
        {
            if (AllItems is null) { return 0; }

            return CurrentIndex + NumberOfItems > AllItems.Count ?
                AllItems.Count : CurrentIndex + NumberOfItems;
        }

        private IList<dynamic> GetPageItems()
        {
            var pageItems = new List<dynamic>();

            for (int i = CurrentIndex; i < LastPageIndex; i++)
            {
                pageItems.Add(AllItems[i]);
            }
            return pageItems;
        }

        public void NextPage()
        {
            if (!CanGoForward) { { return; } }

            CurrentIndex += NumberOfItems;
        }

        public void PreviousPage()
        {
            if (!CanGoBackward) { { return; } }

            CurrentIndex -= NumberOfItems;
        }

        public virtual void OnPageChanged()
        {
            PageChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Load(ICollection<dynamic> allItems, int numberOfItems)
        {
            AllItems = allItems.Cast<dynamic>().ToList();
            CurrentIndex = 0;
            NumberOfItems = numberOfItems;
        }
    }

}

