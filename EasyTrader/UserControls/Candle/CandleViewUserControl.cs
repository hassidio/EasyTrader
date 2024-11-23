using EasyTrader.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyTrader
{
    public partial class CandleViewUserControl : UserControl
    {
        Candle CandleData { get; set; }

        public CandleViewUserControl(Candle candleData)
        {
            InitializeComponent();

            CandleData = candleData;

            //Rectangle r = new Rectangle();
            //r.Width = Width;



        }
    }
}
