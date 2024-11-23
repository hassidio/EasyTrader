using EasyTrader.Core.Views.PropertyView;
using System.Windows.Forms;

namespace EasyTrader.UserControls.PropertyBag
{
    public partial class PropertyBagUserControl : CommonUserControl
    {
        public PropertyBagUserControl()
        {
            InitializeComponent();
            InitializeCommonComponent();
        }

        private ICollection<PropertyUserControl> _propertyUserControls;

        public ICollection<PropertyUserControl> PropertyUserControls
        {
            get { return _propertyUserControls; }
        }

        internal void SetPropertyLayout(int? fontSize = null, Size? size = null)
        {
            if (_propertyUserControls is null) { return; }

            if (fontSize is not null)
            {
                foreach (var propertyUC in _propertyUserControls)
                {
                    propertyUC.SetFont(fontSize);
                }
            }

            if (size is not null)
            {
                foreach (var propertyUC in _propertyUserControls)
                {
                    propertyUC.SetSize(size);
                }
            }
        }

        private TableLayoutPanel _tableLayoutPanelMain;

        internal void Clear()
        {
            _propertyUserControls = new List<PropertyUserControl>();

            _tableLayoutPanelMain = GetNewTableLayoutPanelCollection(1);
            InvokeActionThreadSafe(this, new Action(() => Controls.Clear()));
            AddControlThreadSafe(this, _tableLayoutPanelMain);
        }

        public override void OnLoadPropertyData()
        {
            Clear();

            int height = 0;

            var pd = (PropertyData)PropertyDataObject;

            foreach (var p in pd.Properties)
            {
                var propertyUC = new PropertyUserControl();
                propertyUC.LazyLoad = this.LazyLoad;
                propertyUC.Dock = DockStyle.Top;
                propertyUC.Name += p.PropertyName;
                propertyUC.LoadPropertyData(p);

                _propertyUserControls.Add(propertyUC);

                AddRowTableLayoutPanelThreadSafe(_tableLayoutPanelMain, propertyUC);
                height += propertyUC.Height;

            }

            InvokeActionThreadSafe(
                this, new Action(() => Height = height == 0 ? 0 : height + Padding.Top + Padding.Bottom));

        }

        private void InitializeCommonComponent()
        {

        }
    }
}
