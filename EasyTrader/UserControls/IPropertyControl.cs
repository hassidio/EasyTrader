namespace EasyTrader.UserControls
{
    public interface IPropertyControl
    {
        void LoadPropertyData(dynamic data, string? name = null);

        void OnLoadPropertyData() { }

        PropertyDataUserControl ParentControl { get; set; }

        bool LazyLoad { get; set; }
        dynamic PropertyDataObject { get; set; }
    }
}