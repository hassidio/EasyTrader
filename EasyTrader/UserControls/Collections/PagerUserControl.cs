
namespace EasyTrader.UserControls.Collections
{
    public partial class PagerUserControl : UserControl
    {

        public PagerUserControl()
        {
            InitializeComponent();


            InitializeCommonComponent();
        }

        public Pager Pager { get; set; }

        public void Load(ICollection<dynamic> allItems, int numberOfPageItems)
        {
            Pager.Load(allItems, numberOfPageItems);

            SetPageItems();
        }

        private void InitializeCommonComponent()
        {
            nextUserControl.Load(new Action(() => NextPage()));
            previousUserControl.Load(new Action(() => PreviousPage()));

            nextUserControl.Enabled = false;
            previousUserControl.Enabled = false;

            Pager = new Pager();

        }

        private void SetPageItems()
        {
            if (Pager.LastPageIndex == 0)
            {
                labelPager.Text = string.Empty;
                nextUserControl.Enabled = false;
                previousUserControl.Enabled = false;
                return;
            }

            nextUserControl.Enabled = true;
            previousUserControl.Enabled = true;

            if (!Pager.CanGoBackward)
            {
                previousUserControl.Enabled = false;
            }

            if (!Pager.CanGoForward)
            {
                nextUserControl.Enabled = false;
            }

            labelPager.Text = $"{Pager.CurrentIndex} - {Pager.LastPageIndex} of {Pager.AllItems.Count}";

            Pager.OnPageChanged();
        }

        private void NextPage()
        {
            Pager.NextPage();

            SetPageItems();
        }

        private void PreviousPage()
        {
            Pager.PreviousPage();

            SetPageItems();
        }
    }

}

