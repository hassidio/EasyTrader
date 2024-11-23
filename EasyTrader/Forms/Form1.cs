using EasyTrader.UserControls;
using EasyTrader.Forms;
using EasyTrader.Core.Views.PropertyView;
using EasyTrader.UserControls.PropertyBag;


namespace EasyTrader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var binanceView = new BinanceData();

            //var exchange = binanceView.Trader.Tasks.DefaultExchange;
            //var market = binanceView.Trader.Tasks.GetMarket<BinanceMarket>("ETHBTC");

            //exchange.ClientDataTable.ChildTables;

            //DataGridView dgv = new DataGridView();
            //dataGridView1.EntityId = "dataGridView1";
            //panelSandbox.Controls.Add(dataGridView1);

            //dataGridView1.DataSource = new PropertyDataTable(new MockObject(), "MockConfigurations");
            //dataGridView1.DataSource = market.ClientPropertyDataTable;
            //dataGridView1.DataSource = market.ClientCommonDataTable;

            //dataGridView1.DataSource = market.GetCandles<BinanceCandle>();//.ClientPropertyDataTable;

            var obj = new MockObject();
            var pd = new PropertyData(obj);
            PropertyData dogTag = pd.ChildObjects.First();

            //PropertyUserControl1.LoadPropertyData(dogTag.Properties.First());

            PropertyBagUserControl1.LoadPropertyData(obj);
            ExpandCollapsUserControl1.LabelTitle.Text = "Oren";
            //ExpandCollapsUserControl1.Size = new Size(700, 400);
            //PropertyBagUserControl1.Width = 400;
            //PropertyBagUserControl1.Size = new Size(310, 50);

            //label1.Font = new Font(label1.Font.EntityId, 24F);

        }

        private PropertyUserControl PropertyUserControl1 { get; set; }
        private PropertyBagUserControl PropertyBagUserControl1 { get; set; }
        private PropertyDataUserControl ExpandCollapsUserControl1 { get; set; }

        private void InitializeCustomComponent()
        {
            SuspendLayout();
            
            //dataGridView1.ColumnHeadersVisible = false;

            PropertyUserControl1 = new PropertyUserControl();

            PropertyBagUserControl1 = new PropertyBagUserControl();

            ExpandCollapsUserControl1 = new PropertyDataUserControl();

            //Controls.Add(PropertyUserControl1);

            //Controls.Add(PropertyBagUserControl1);

            //ExpandCollapsUserControl1.FlowPanelContent.Controls.Add(PropertyBagUserControl1);

            Controls.Add(ExpandCollapsUserControl1);

            ResumeLayout(false);

            //Candles
            //object[] candleData = [1725250500000, "0.04245000", "0.04245000", "0.04235000", "0.04238000", "96.01390000", 1725251399999, "4.07281734", 552, "28.30200000", "1.20051460", "0"];

            //var candle = new BinanceCandle();
            //candle.OnSetCandle(new JArray(candleData));

            //var candleview = new CandleViewUserControl(candle);
            //candleview.Location = new Point(273, 125);
            //candleview.EntityId = "candleview";
            //candleview.Size = new Size(250, 183);
            //candleview.TabIndex = 0;

            //Controls.Add(candleview);


        }

        private void button1_Click(object sender, EventArgs e)
        {

            //DialogResult mb =  MessageBox.Show(ExpandCollapsUserControl1.LabelTitle.Text, "Title");

            var c = ExpandCollapsUserControl1;
            ExpandCollapsUserControl1.Size = new Size(
                c.Width + 100,
                c.Height + 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var formMain = new Main();
            formMain.Show();
        }
    }
}
