

namespace EasyTrader.UserControls.ActionButtons
{
    public partial class OpenPropertyButton : UserControl
    {
        public OpenPropertyButton()
        {
            InitializeComponent();
        }


        public void Load(Action actionMouseClick)
        {
            ActionMouseClick = actionMouseClick;
        }

        public Action ActionMouseClick { get; set; }



        private void pictureBoxOpen_MouseClick(object sender, MouseEventArgs e)
        {
            if (ActionMouseClick is null) { return; }
            ActionMouseClick.Invoke();
        }

        private void pictureBoxOpen_MouseHover(object sender, EventArgs e)
        {
            pictureBoxOpen.Image = Properties.Resources.Open_on;

        }

        private void pictureBoxOpen_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxOpen.Image = Properties.Resources.Open_40;

        }
    }
}
