using EasyTrader.Core.Views.OutputView.OutputWindowView;
using System.Diagnostics;

namespace EasyTrader.Tests.Models.Views
{
    public class MockClientOutputWindowControl : IClientOutputWindow
    {
        public MockClientOutputWindowControl()
        {
            OutputWindow = new OutputWindow(this);
        }

        public OutputWindow OutputWindow { get; set; }


        public void UpdateOutputWindow(OutputWindow outputWindow)
        {
            OutputWindow = outputWindow;

            Debug.WriteLine($"");
            Debug.WriteLine($"OutputWindow at {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}");
            foreach (var item in OutputWindow)
            {
                Debug.WriteLine($"[{item.DateTime.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}] OutputWindowId: {item.Id}; '{item.Text}' >>>> {item.Status.ToString()}");
            }
        }
    }
}

