namespace EasyTrader.UserControls.Collections
{
    public class CollectionItemSelectionChangedEventArgs : EventArgs
    {
        public CollectionItemSelectionChangedEventArgs(ICollectionItemUserControl item, int itemIndex, bool isSelected)
        {
            Item = item;
            ItemIndex = itemIndex;
            IsSelected = isSelected;
        }

        /// <summary>
        ///  The list view item whose selection changed
        /// </summary>
        public ICollectionItemUserControl Item { get; }

        /// <summary>
        ///  The list view item's index
        /// </summary>
        public int ItemIndex { get; }

        /// <summary>
        ///  Return true if the item is selected
        /// </summary>
        public bool IsSelected { get; }
    }
}
