using EasyTrader.Core.Common;
using EasyTrader.Core.Views.OutputView.ApiThrottleWindowView;
using EasyTrader.UserControls.PropertyBag;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace EasyTrader.UserControls.ApiThrottle
{
    public partial class ApiThrottleUserControl : UserControl, IClientApiThrottleWindow
    {
        public ApiThrottleUserControl()
        {
            InitializeComponent();

            InitializeCommonComponent();

            _apiThrottleWindow = new ApiThrottleWindow(this);
        }

        private ApiThrottleWindow _apiThrottleWindow;


        public ApiThrottleWindow ApiThrottleWindow
        {
            get
            {
                if (_apiThrottleWindow is null) { _apiThrottleWindow = new ApiThrottleWindow(this); }
                return _apiThrottleWindow;
            }
            set => _apiThrottleWindow = value;
        }

        public string Title { get; set; }

        public void UpdateApiThrottleWindow(Throttle throttle)
        {
            throttlePropertyBagUserControl.LoadPropertyData(throttle);

            SetIndicatorPanel(throttle);

            CommonUserControl.InvokeActionThreadSafe(
                throttlePropertyBagUserControl,
                new Action(() => { throttlePropertyBagUserControl.SetPropertyLayout(fontSize: 7); }));

            var sizeP = new Size(300, 22);
            CommonUserControl.InvokeActionThreadSafe(
                throttlePropertyBagUserControl,
                new Action(() => { throttlePropertyBagUserControl.SetPropertyLayout(size: sizeP); }));
        }

        private void SetIndicatorPanel(Throttle throttle)
        {
            float usedWeightP;
            float remainingWeightP;

            if (throttle.CurrentWeightUsed <= throttle.MaximumTotalWeight)
            {
                usedWeightP = (throttle.CurrentWeightUsed / (float)throttle.MaximumTotalWeight) * 100;
                remainingWeightP = 100 - usedWeightP;
            }
            else
            {
                usedWeightP = 100;
                remainingWeightP = 0;
            }

            SetIndicatorThreadSafe(tableLayoutPanelIndicator, 0, usedWeightP);
            SetIndicatorThreadSafe(tableLayoutPanelIndicator, 1, remainingWeightP);
        }

        public void SetIndicatorThreadSafe(TableLayoutPanel tableLayoutPanel, int index, float width)
        {
            CommonUserControl.InvokeActionThreadSafe(
                tableLayoutPanel, 
                new Action(() => { tableLayoutPanel.ColumnStyles[index].Width = width; }));

        }

        public void InitializeCommonComponent()
        {

        }



    }
}
