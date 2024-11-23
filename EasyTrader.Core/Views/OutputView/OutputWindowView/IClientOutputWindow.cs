namespace EasyTrader.Core.Views.OutputView.OutputWindowView
{
    public interface IClientOutputWindow
    {
        OutputWindow OutputWindow { get; set; }

        void UpdateOutputWindow(OutputWindow outputWindow);
    }
}
