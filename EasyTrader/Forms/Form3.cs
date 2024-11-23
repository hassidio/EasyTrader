using EasyTrader.Core.Common;
using EasyTrader.Core.Configuration;
using EasyTrader.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyTrader.Forms
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public Throttle Throttle { get; set; }
        private void Form3_Load(object sender, EventArgs e)
        {
            var apiConfiguration = new ApiConfiguration
            {
                CallsPerSecondPolicy = 10,
                WeightCooldownSecondsPolicy = 5,
                MaximumTotalWeight = 6000
            };

            Throttle = new Throttle(apiConfiguration);
            Throttle.AddWeight(150);

            //propertyBagUserControl1.LoadPropertyData(throttle);
            apiThrottleUserControl1.UpdateApiThrottleWindow(Throttle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Throttle.AddWeight(150);
            apiThrottleUserControl1.UpdateApiThrottleWindow(Throttle);
        }
    }
}
