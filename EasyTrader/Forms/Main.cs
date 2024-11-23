using EasyTrader.Core;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Tests.Models;
using EasyTrader.Tests.UnitTests;
using System.Diagnostics;
using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Core.Views.OutputView;
using EasyTrader.Core.Models.Binance;
using EasyTrader.Services.DataServices;
using EasyTrader.UserControls.ApiThrottle;
using EasyTrader.UserControls.MarketList;
using EasyTrader.UserControls.Collections;
using EasyTrader.Core.DataServices;


namespace EasyTrader.Forms
{
    public partial class Main : Form
    {
        public string ConfigurationDirectoryPath { get { return @"..\..\..\"; } } //*/


        public Main()
        {
            var configurations = TraderGlobals.ReadConfigurations(ConfigurationDirectoryPath);

            TraderDataManager = new TraderDataManager(
                exchangeNames: TraderGlobals.Portfolio.ExchangeNames,
                outputWindows: outputUserControl1);

            InitializeComponent();

            InitializeCustomComponent();

        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Exchange DropdownList
            comboBoxExchange.DataSource = TraderGlobals.Portfolio.ExchangeNames;
        }

        public TraderDataManager TraderDataManager { get; set; }

        private void comboBoxExchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMarkets(((ComboBox)sender).SelectedItem.ToString());
        }

        private void SetMarkets(string exchangeName)
        {
            var exchangeDataManager = new ExchangeDataManager(
                TraderDataManager.GetTraderDataService(exchangeName));

            marketListUserControl.LoadCollectionData(exchangeDataManager, exchangeDataManager.MarketNames);

            marketListUserControl.SelectionChanged += MarketListUserControl_Selected;
        }

        private void MarketListUserControl_Selected(object? sender, CollectionItemSelectionChangedEventArgs e)
        {

            MessageBox.Show($"ItemSelected: {comboBoxExchange.SelectedItem.ToString()}, {e.Item.Data}, {e.ItemIndex}");
        }



        private void InitializeCustomComponent()
        {
            // market List User Control
            marketListUserControl.Title.Text = "Markets:";
            marketListUserControl.BackColor = DefaultBackColor;

            // api Throttle Controls Tabs
            foreach (var item in TraderDataManager.ClientOutputDictionary)
            {
                var apiThrottleControl = (ApiThrottleUserControl)item.Value.ClientApiThrottleWindow;

                apiThrottleControl.Dock = DockStyle.Fill;

                TabPage tabPage = new TabPage(); 
                tabPage.Padding = new Padding(3);
                tabPage.Text = apiThrottleControl.Title;
                tabPage.UseVisualStyleBackColor = true;

                tabPage.Controls.Add(apiThrottleControl);

                tabControlApiThrottle.Controls.Add(tabPage);
            }


            //outputUserControl1.labelOutputWindow.Text = "Come back here... no output";
        }



        //*************************************************************************************************************************************************************************************


        private async void buttonTestBinanceOutput_Click(object sender, EventArgs e)
        {
            var exchangeConfiguration =
                TraderGlobals.Portfolio.GetExchangeConfiguration("Binance");

            var binanceData = new BinanceData(
                exchangeConfiguration,
                new ClientOutput(outputUserControl1, TraderDataManager.ClientOutputDictionary["Binance"].ClientApiThrottleWindow));


            var marketId = "ETHBTC";
            var interval = "15m";
            var limit = "100";

            var marketRequestOptions = new ApiRequestOptions(exchangeConfiguration)
            {
                ApiRequestQueryOptions =
                new Dictionary<string, string>
                {
                    { "symbol", marketId  },
                    { "interval", interval },
                    { "limit", limit },
                }
            };

            var exchangeRequestOptions = new ApiRequestOptions(exchangeConfiguration);


            var sleep1 = 1000;
            var sleep2 = 100;

            //TraderGlobals.OutputWindow.WriteOutputWindowItem(text: "Main Thread 1");

            BinanceExchange exchange =
            await binanceData.Trader.Tasks.UpdateExchangeInfoAsync(exchangeRequestOptions);

            //TraderGlobals.OutputWindow.WriteOutputWindowItem(text: "Main Thread 2");

            //var clientInfo = (BinanceApi_ExchangeInfo)exchange.RawClientData;

            //TraderGlobals.OutputWindow.WriteOutputWindowItem(text: $"{clientInfo.rateLimits[0].limit}");


            //TraderGlobals.OutputWindow.WriteOutputWindowItem(text: "Main Thread end", status: OutputItemStatusEnum.Fail);


            /*
             [x-mbx-used-weight]
            [x-mbx-used-weight-1m]
            */
        }


        private void PrintHeaders(string v, IDictionary<string, IEnumerable<string>> headers)
        {
            foreach (var h in headers)
            {
                Debug.WriteLine($"[{h.Key}] = {h.Value}");
            }
        }

        private async void buttonTestOutput_Click(object sender, EventArgs e)
        {
            // Setup
            outputUserControl1.ClearOutputWindow();

            var exchangeConfiguration = TraderGlobals.Portfolio.GetExchangeConfiguration("Mock Exchange Name");

            var mockTraderData = new MockTraderData(
                exchangeConfiguration,
                new ClientOutput(outputUserControl1, TraderDataManager.ClientOutputDictionary["Mock Exchange Name"].ClientApiThrottleWindow));

            ((MockApi)mockTraderData.Trader.ApiController.Api).OnSendSleepMilliseconds = 2000;

            //Test

            mockTraderData.Trader.ClientOutput.ClientOutputWindow.OutputWindow.WriteOutputWindowItem(
                text: "Main Thread start", status: OutputItemStatusEnum.Success);

            MockExchange exchange = await mockTraderData.Trader.Tasks.UpdateExchangeInfoAsync(UnitTestBase.NewApiRequestOptions());

            for (int i = 0; i < 10; i++)
            {
                //MockMarket market = await mockView.Trader.Tasks.UpdateMarketCandlesProcess(exchange.MarketNames.ToList()[i].), UnitTestBase.NewApiRequestOptions());

            }

            mockTraderData.Trader.ClientOutput.ClientOutputWindow.OutputWindow.WriteOutputWindowItem(text: "Main Thread end", status: OutputItemStatusEnum.Success);

        }
        private async void buttonError_Click(object sender, EventArgs e)
        {
            // Setup
            outputUserControl1.ClearOutputWindow();

            var exchangeConfiguration = TraderGlobals.Portfolio.GetExchangeConfiguration("Mock Exchange Name");

            var mockTraderData = new MockTraderData(
                exchangeConfiguration,
                new ClientOutput(outputUserControl1, null));

            ((MockApi)mockTraderData.Trader.ApiController.Api).ExceptionCode = 404;

            //Test
            mockTraderData.Trader.ClientOutput.ClientOutputWindow.OutputWindow.WriteOutputWindowItem(
                text: "Main Thread start", status: OutputItemStatusEnum.Success);

            MockExchange exchange = await mockTraderData.Trader.Tasks.UpdateExchangeInfoAsync(
                UnitTestBase.NewApiRequestOptions());

            mockTraderData.Trader.ClientOutput.ClientOutputWindow.OutputWindow.WriteOutputWindowItem(
                text: "Main Thread end", status: OutputItemStatusEnum.Success);

        }





        private void buttonForm2_Click(object sender, EventArgs e)
        {
            var f = new Form2();
            f.Show();
        }

        private void buttonForm3_Click(object sender, EventArgs e)
        {
            var f = new Form3();
            f.Show();
        }

        private void buttonTestOutputWindow_Click(object sender, EventArgs e)
        {
            var ow = new OutputWindow(outputUserControl1);

            var owi1 = new OutputWindowItem
            {
                DateTime = DateTime.Now,
                Id = "1",
                Status = OutputItemStatusEnum.Success,
                Text = "Tests 1"
            };

            var owi2 = new OutputWindowItem
            {
                DateTime = DateTime.Now,
                Id = "2",
                Status = OutputItemStatusEnum.Fail,
                Text = "A Tests 2: These terms will be used throughout the documentation, \r\n" +
                "B so it is recommended especially for new users to read to help their understanding of the API.\r\n" +
                "C Tests 2: These terms will be used throughout the documentation, \r\n" +
                "D so it is recommended especially for new users to read to help their understanding of the API.\r\n" +
                "E Tests 2: These terms will be used throughout the documentation, \r\n" +
                "F so it is recommended especially for new users to read to help their understanding of the API.\r\n" +
                "Tests 2: These terms will be used throughout the documentation, \r\n" +
                "so it is recommended especially for new users to read to help their understanding of the API.\r\n"
            };

            var owi3 = new OutputWindowItem
            {
                DateTime = DateTime.Now,
                Id = "3",
                Status = OutputItemStatusEnum.InProgress,
                Text = "A Tests 3: These terms will be used throughout the documentation, " +
                "B so it is recommended especially for new users to read to help their understanding of the API. " +
                "C Tests 2: These terms will be used throughout the documentation, " +
                "D so it is recommended especially for new users to read to help their understanding of the API." +
                "E Tests 2: These terms will be used throughout the documentation, " +
                "F so it is recommended especially for new users to read to help their understanding of the API. " +
                "Tests 2: These terms will be used throughout the documentation, " +
                "so it is recommended especially for new users to read to help their understanding of the API."
            };

            ow.Add(owi1);
            ow.Add(owi2);
            ow.Add(owi3);
        }


    }





}
