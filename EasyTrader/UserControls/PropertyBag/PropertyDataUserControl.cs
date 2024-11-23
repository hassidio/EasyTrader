using EasyTrader.Core.Views.PropertyView;
using EasyTrader.UserControls.Collections;
using EasyTrader.UserControls.MarketList;
using EasyTrader.UserControls.PropertyBag;

namespace EasyTrader.UserControls
{
    public partial class PropertyDataUserControl : CommonUserControl, ICollectionItemUserControl
    {
        public PropertyDataUserControl()
        {
            InitializeComponent();
            InitializeCommonComponent();

            SetLayout();
        }

        //public static void PrintControl(Control control, string clientId = "*")
        //{
        //    Debug.WriteLine($"<{clientId}> {control.Name}: Size: {control.Size} Dock: {control.Dock} Pagging: {control.Padding} Margin: {control.Margin} ClientSize: {control.ClientSize} PreferredSize:{control.PreferredSize}");
        //}

        private bool _isContentVisible = false;

        //Pager
        public int NumberOfPageItems { get; set; }

        public Label LabelTitle { get { return labelTitle; } }

        //Parent
        public override PropertyDataUserControl ParentControl { get; set; }

        // Property Data Controls
        public ICollection<PropertyDataUserControl> PropertyDataUserControls
        {
            get { return _propertyDataUserControls; }
        }

        // Property Bag
        private PropertyBagUserControl _propertyBagUserControl;

        public PropertyBagUserControl PropertyBagUserControl
        {
            get { return _propertyBagUserControl; }
        }

        public ICollection<PropertyUserControl> PropertyUserControls
        {
            get { return _propertyBagUserControl.PropertyUserControls; }
        }

        public PagerCollection PagerCollection { get; private set; }

        // Objects
        private TableLayoutPanel _tableLayoutPanelObjects;

        private ICollection<PropertyDataUserControl> _propertyDataUserControls;



        public override void OnLoadPropertyData()
        {
            CreatePropertyControls();

            if (!LazyLoad) { Toggle(); }
        }

        private void CreatePropertyControls()
        {
            _suspendLayout();

            var propertyData = (PropertyData)PropertyDataObject;

            labelTitle.Text = propertyData.PropertyNameView;

            // Properties Bag
            _propertyBagUserControl = GetPropertyBagUserControl(propertyData);
            _propertyBagUserControl.LoadPropertyData(propertyData);

            // Objects
            _propertyDataUserControls = new List<PropertyDataUserControl>();

            pagerUserControl1.Visible = false;

            if (propertyData.ChildObjects.Count <= NumberOfPageItems)
            {
                foreach (var pd in propertyData.ChildObjects)
                {
                    var propertyObjectUC = GetPropertyDataUserControl(pd);
                    _propertyDataUserControls.Add(propertyObjectUC);
                }
            }
            else
            {
                pagerUserControl1.Visible = true;

                PagerCollection = new PagerCollection(pagerUserControl1, NumberOfPageItems);
                PagerCollection.LoadCollectionData(propertyData.ChildObjects.Cast<dynamic>().ToList());
                PagerCollection.CollectionPageChanged += PagerCollection_CollectionPageChanged;

                foreach (var pd in PagerCollection.PageItems)
                {
                    var propertyObjectUC = GetPropertyDataUserControl(pd);
                    _propertyDataUserControls.Add(propertyObjectUC);
                }
            }

            SetDataLayout();
        }

        private void PagerCollection_CollectionPageChanged(object? sender, EventArgs e)
        {
            _propertyDataUserControls = new List<PropertyDataUserControl>();

            foreach (var pd in PagerCollection.PageItems)
            {
                var propertyObjectUC = GetPropertyDataUserControl(pd);
                _propertyDataUserControls.Add(propertyObjectUC);
            }

            SetDataLayout();
        }

        public void Clear()
        {
            // Properties bag
            panelProperties.Controls.Clear();

            // Objects
            panelObjets.Controls.Clear();
            _tableLayoutPanelObjects = GetNewTableLayoutPanelCollection(1);
            panelObjets.Controls.Add(_tableLayoutPanelObjects);
        }

        private PropertyBagUserControl GetPropertyBagUserControl(PropertyData propertyData)
        {
            PropertyBagUserControl control =
                GetPropertiesControl<PropertyBagUserControl>(propertyData);

            return control;
        }

        private PropertyDataUserControl GetPropertyDataUserControl(PropertyData propertyData)
        {
            PropertyDataUserControl control =
                GetPropertiesControl<PropertyDataUserControl>(propertyData);

            control.NumberOfPageItems = NumberOfPageItems;
            //control.SetLayout();

            control.labelTitle.Text = propertyData.PropertyNameView;

            return control;
        }

        private T GetPropertiesControl<T>(PropertyData propertyData) where T : Control, IPropertyControl, new()
        {
            return new T()
            {
                Dock = DockStyle.Top,
                Name = $"propertyUserControl{propertyData.PropertyNameView}",
                ParentControl = this,
                LazyLoad = this.LazyLoad,
                PropertyDataObject = propertyData,
            };
        }

        public void SetDataLayout()
        {
            _suspendLayout();

            Clear();

            // Property Bag
            if (_propertyBagUserControl is not null)
            {
                panelProperties.Controls.Add(_propertyBagUserControl);
            }

            // Objects
            if (_propertyDataUserControls is not null)
            {
                foreach (var propertyObjectUC in _propertyDataUserControls)
                {
                    AddRowTableLayoutPanelThreadSafe(_tableLayoutPanelObjects, propertyObjectUC);
                }
            }
            //_ResumeLayout();

            SetLayout();
        }

        public void SetLayout()
        {
            _suspendLayout();

            panelProperties.Height = 0;
            panelObjets.Height = 0;

            if (_isContentVisible)
            {
                // Property Bag
                if (_propertyBagUserControl is not null)
                {
                    panelProperties.Height = _propertyBagUserControl.Height == 0 ? 0 :
                    _propertyBagUserControl.Height + panelProperties.Padding.Top + panelProperties.Padding.Bottom;
                }

                // Objects
                int height = GetInnerObjectsHeight();
                panelObjets.Height = height == 0 ? 0 : height + panelObjets.Padding.Top + panelObjets.Padding.Bottom;
            }

            tableLayoutPanelData.Height =
                panelProperties.Height +
                panelObjets.Height +
                (pagerUserControl1.Visible ? pagerUserControl1.Height : 0);

            tableLayoutPanelContent.Height =
                tableLayoutPanelData.Height == 0 ? 0 :
                tableLayoutPanelData.Height + tableLayoutPanelData.Margin.Top + tableLayoutPanelData.Margin.Bottom;

            tableLayoutPanelMain.Height =
                tableLayoutPanelTop.Height + tableLayoutPanelContent.Height;

            Height = tableLayoutPanelMain.Height + Padding.Top + Padding.Bottom;

            if (ParentControl is not null)
            {
                ParentControl.SetLayout();
            }

            _ResumeLayout();
        }


        private bool isSuspendLayout = false;
        private void _suspendLayout()
        {
            if (isSuspendLayout) { return; }

            this.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelContent.SuspendLayout();
            tableLayoutPanelData.SuspendLayout();
            panelProperties.SuspendLayout();
            panelObjets.SuspendLayout();
            if (_propertyBagUserControl is not null) { _propertyBagUserControl.SuspendLayout(); }
            if (_tableLayoutPanelObjects is not null) { _tableLayoutPanelObjects.SuspendLayout(); }
            if (pagerUserControl1.Visible) { pagerUserControl1.SuspendLayout(); }

            isSuspendLayout = true;
        }
        private void _ResumeLayout()
        {
            if (!isSuspendLayout) { return; }

            if (pagerUserControl1.Visible) { pagerUserControl1.ResumeLayout(); }
            if (_tableLayoutPanelObjects is not null) { _tableLayoutPanelObjects.ResumeLayout(); }
            if (_propertyBagUserControl is not null) { _propertyBagUserControl.ResumeLayout(); }
            panelObjets.ResumeLayout();
            panelProperties.ResumeLayout();
            tableLayoutPanelData.ResumeLayout();
            tableLayoutPanelContent.ResumeLayout();
            tableLayoutPanelMain.ResumeLayout();
            this.ResumeLayout();

            isSuspendLayout = false;
        }

        private int GetInnerObjectsHeight()
        {
            int height = 0;

            if (_propertyDataUserControls is null) { return height; }

            foreach (var propertyObjectUC in _propertyDataUserControls)
            {
                height += propertyObjectUC.Height;
            }

            return height;
        }

        private void InitializeCommonComponent()
        {
            //open close object
            openCloseUserControl.ImageOn = Properties.Resources.Plus_On;
            openCloseUserControl.ImageOff = Properties.Resources.Plus_40;
            openCloseUserControl.Load(new Action(() => Toggle()));

            // paneland table Objets
            _tableLayoutPanelObjects = GetNewTableLayoutPanelCollection(1);
            panelObjets.Controls.Add(_tableLayoutPanelObjects);
            pagerUserControl1.Visible = false;
        }


        internal void Toggle()
        {
            _isContentVisible = !_isContentVisible;
            if (!_isContentVisible)
            {
                SetLayout();
                openCloseUserControl.ImageOn = Properties.Resources.Plus_On;
                openCloseUserControl.ImageOff = Properties.Resources.Plus_40;
            }
            else
            {
                CreatePropertyControls();
                openCloseUserControl.ImageOn = Properties.Resources.Minus_on;
                openCloseUserControl.ImageOff = Properties.Resources.Minus_40;
            }
        }


        #region ICollectionItemUserControl

        public void LoadCollectionData(dynamic data, int index)
        {

        }

        //Not in use
        public event EventHandler<CollectionItemSelectionChangedEventArgs> ItemSelected;

        //Not in use
        public dynamic Data => throw new NotImplementedException();

        //Not in use
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //Not in use
        public int Index { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //Not in use
        public void ClearSelection() { throw new NotImplementedException(); }

        #endregion
    }
}
