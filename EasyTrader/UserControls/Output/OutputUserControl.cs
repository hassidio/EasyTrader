using EasyTrader.Core.Views.OutputView.OutputWindowView;

namespace EasyTrader.UserControls.Output
{
    public partial class OutputUserControl : CommonUserControl, IClientOutputWindow
    {
        public OutputUserControl()
        {
            InitializeComponent();

            InitializeCommonComponent();

            _outputWindow = new OutputWindow(this);

        }

        private OutputWindow _outputWindow;
        public OutputWindow OutputWindow
        {
            get
            {
                if (_outputWindow is null) { _outputWindow = new OutputWindow(this); }
                return _outputWindow;
            }
            set => _outputWindow = value;
        }

        private ICollection<OutputItemUserControl> _outputItemUserControls;
        public ICollection<OutputItemUserControl> OutputItemUserControls
        {
            get { return _outputItemUserControls; }
        }

        internal void ClearOutputWindow()
        {
            if (OutputWindow is not null)
            { OutputWindow.Clear(); }
        }

        public void UpdateOutputWindow(OutputWindow outputWindow)
        {
            //lock (OutputWindow)
            //{
                OutputWindow = outputWindow;

                ClearTableLayoutPanelThreadSafe(tableLayoutPanelContent);

                _outputItemUserControls = new List<OutputItemUserControl>();

                foreach (var outputItem in OutputWindow)
                {
                    var outputItemUC = new OutputItemUserControl();
                    outputItemUC.Name = "OutputItemUserControl" + outputItem.Id;
                    outputItemUC.Dock = DockStyle.Top;
                    outputItemUC.Load(outputItem);
                    AddControlThreadSafe(tableLayoutPanelContent, outputItemUC);
                    _outputItemUserControls.Add(outputItemUC);
                }
            //}
        }

        public void InitializeCommonComponent()
        {
        }

    }
}
