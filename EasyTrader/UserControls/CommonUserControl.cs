using EasyTrader.Core.Processes;
using EasyTrader.Core.Views.PropertyView;
using EasyTrader.Services.DataServices;


namespace EasyTrader.UserControls
{
    public partial class CommonUserControl : UserControl, IPropertyControl
    {
        public CommonUserControl()
        {
            InitializeComponent();

            InitializeCommonUserControlComponent();
        }

        public dynamic PropertyDataObject { get; set; }
        public virtual PropertyDataUserControl ParentControl { get; set; }

        public bool LazyLoad { get; set; }

        private void InitializeCommonUserControlComponent()
        {
        }

        public void LoadPropertyData(dynamic data, string? name = null)
        {
            // Set Property DataO bject
            if (data.GetType() == typeof(Property)
                            || data.GetType() == typeof(PropertyData))
            { PropertyDataObject = data; }
            else { PropertyDataObject = new PropertyData(obj: data, name: name); }

            OnLoadPropertyData();
        }

        public virtual void OnLoadPropertyData() { }


        public void SetPropertyLayout(int? fontSize = null, Size? size = null)
        {
            SetFont(fontSize);
            SetSize(size);
        }

        public virtual void SetFont(int? fontSize = null) { }

        public virtual void SetSize(Size? size = null) { }


        protected int GetControlTextHeight(Control control)
        {
            Size boxSize = new Size(control.Width, Int32.MaxValue);
            var size = TextRenderer.MeasureText(control.Text, control.Font, boxSize, TextFormatFlags.WordBreak);
            return size.Height;
        }

        protected int GetControlTextWidth(Control control)
        {
            Size boxSize = new Size(control.Width, Int32.MaxValue);
            var size = TextRenderer.MeasureText(control.Text, control.Font, boxSize, TextFormatFlags.WordBreak);
            return size.Width;
        }

        public static TableLayoutPanel GetNewTableLayoutPanelCollection(int columnCount = 0, ColumnStyle? columnStyle = null, string name = "")
        {
            var tableLayoutPanel = new TableLayoutPanel()
            {
                BackColor = Color.Transparent,
                Dock = DockStyle.Fill,
                Name = "tableLayoutPanelCollection_" + name,
                Size = new Size(500, 500),
                Margin = new Padding(0),
                Padding = new Padding(0),
            };

            if (columnCount > 0)
            {
                if (columnStyle is null) { columnStyle = new ColumnStyle(SizeType.Percent, 100F); }

                tableLayoutPanel.ColumnCount = columnCount;
                for (int i = 0; i < columnCount; i++)
                {
                    tableLayoutPanel.ColumnStyles.Add(columnStyle);
                }
            }

            return tableLayoutPanel;
        }


        public static void ClearControlsThreadSafe(Control control)
        {
            var action = new Action(() => { control.Controls.Clear(); });
            InvokeActionThreadSafe(control, action);
        }

        public static void AddControlThreadSafe(Control controlParent, Control controlChild)
        {
            var action = new Action(() => { controlParent.Controls.Add(controlChild); });
            InvokeActionThreadSafe(controlParent, action);
        }

        public static void ClearTableLayoutPanelThreadSafe(
            TableLayoutPanel tableLayoutPanel)
        {
            InvokeActionThreadSafe(tableLayoutPanel, new Action(() =>
            {
                tableLayoutPanel.RowCount = 0;
                tableLayoutPanel.RowStyles.Clear();
            }));

            ClearControlsThreadSafe(tableLayoutPanel);
        }

        public static void AddRowTableLayoutPanelThreadSafe(
            TableLayoutPanel tableLayoutPanel,
            Control controlChild,
            RowStyle? rowStyle = null)
        {
            if (rowStyle == null) { rowStyle = new RowStyle(); }

            var action = new Action(() =>
            {
                tableLayoutPanel.RowCount++;
                tableLayoutPanel.RowStyles.Add(rowStyle);
                tableLayoutPanel.Controls.Add(controlChild);
            });

            InvokeActionThreadSafe(tableLayoutPanel, action);
        }

        public static void InvokeActionThreadSafe(Control control, Action action)
        {
            //control.SuspendLayout();

            if (control.InvokeRequired) { control.Invoke(action); }
            else { action.Invoke(); }

            //control.ResumeLayout(false);

        }


    }
}
