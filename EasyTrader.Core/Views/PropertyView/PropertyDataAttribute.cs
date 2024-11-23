namespace EasyTrader.Core.Views.PropertyView
{
    public class PropertyDataAttribute : Attribute
    {
        public PropertyDataAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }
    }
}
