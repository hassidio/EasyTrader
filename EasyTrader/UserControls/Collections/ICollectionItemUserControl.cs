namespace EasyTrader.UserControls.Collections
{
    public interface ICollectionItemUserControl
    {
        dynamic Data { get; }

        bool IsSelected { get; set; }

        int Index { get; set; }

        event EventHandler<CollectionItemSelectionChangedEventArgs> ItemSelected;

        void LoadCollectionData(dynamic data, int index);

        void SetPropertyLayout(int? fontSize = null, Size? size = null);

        void ClearSelection();

    }
}
