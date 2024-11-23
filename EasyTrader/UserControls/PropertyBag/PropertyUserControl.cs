using EasyTrader.Core.Views.PropertyView;


namespace EasyTrader.UserControls.PropertyBag
{
    public partial class PropertyUserControl : CommonUserControl
    {
        public PropertyUserControl()
        {
            InitializeComponent();
            InitializeCommonComponent();
        }

        public Type PropertyType { get; set; }

        public Control DynamicControl { get; set; }


        public override void OnLoadPropertyData()
        {
            var property = (Property)PropertyDataObject as Property;

            if (property.IsValue || property.PropertyValue is null)
            {
                // Dynamic Control 
                DynamicControl = GetDynamicControl(PropertyDataObject.GetType());

                if (DynamicControl is not null)
                {
                    DynamicControl.Text =
                        property.PropertyValue is null ?
                        string.Empty : property.PropertyValue.ToString();

                    DynamicControl.Name = "DynamicControl" + property.PropertyName;
                }

                labelPropertyName.Text = property.PropertyDisplayName;
                PropertyType = property.PropertyType;

                tableLayoutPanelMain.Controls.Add(DynamicControl, 1, 0);
            }
        }

        private Control GetDynamicControl(Type controlType)
        {
            var control = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Name = "PropertyTextBox",
                BackColor = Constants.DefaultBackgroundColor,
                Font = new Font(Font.Name, Constants.DefaultFontSize),
                AutoSize = true,
            };

            //if (controlType == typeof(DateTime))
            //{
            //    throw new NotImplementedException();
            //}

            return control;
        }

        private void InitializeCommonComponent()
        {
        }

        public void SetFont(int? fontSize = null)
        {
            if (fontSize is not null)
            {
                labelPropertyName.Font = new Font(labelPropertyName.Font.Name, (int)fontSize);
                DynamicControl.Font = new Font(DynamicControl.Font.Name, (int)fontSize);
            }
        }

        public void SetSize(Size? size = null)
        {
            if (size is not null)
            {
                Size = (Size)size;
            }
        }
    }
}
