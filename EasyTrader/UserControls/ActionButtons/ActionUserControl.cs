

namespace EasyTrader.UserControls.ActionButtons
{
    public partial class ActionUserControl : UserControl
    {
        public ActionUserControl()
        {
            InitializeComponent();

        }

        public void Load( Action action)
        {
            Action = action;

            pictureBoxOpen.Image = ImageOff;
        }

        public Action Action { get; set; }
        public Image ImageOn { get; set; }
        public Image ImageOff { get; set; }
        public Panel ImagePanel { get; set; }

        private void pictureBoxOpen_MouseClick(object sender, MouseEventArgs e)
        {
            if (Action is null) { return; }
            Action.Invoke();
        }

        private void pictureBoxOpen_MouseHover(object sender, EventArgs e)
        {
            pictureBoxOpen.Image = ImageOn;

        }

        private void pictureBoxOpen_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxOpen.Image = ImageOff;

        }

    }
}
