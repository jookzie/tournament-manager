using BLL;
using Modules.Enums;
using Modules.Interfaces.BLL;

namespace DesktopApp
{
    public partial class Login : Form
    {
        private Authenticator _authenticator;
        private IUserManager _userManager;
        public Login(Authenticator authenticator, IUserManager userManager)
        {
            InitializeComponent();
            _authenticator = authenticator;
            _userManager = userManager;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_email.Text) ||
                string.IsNullOrEmpty(tb_password.Text))
            {
                MessageBox.Show("Email and password cannot be empty.", "Empty fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var user = _userManager.GetUserBy(tb_email.Text);
            if (user is null || !_authenticator.Authenticate(tb_email.Text, tb_password.Text))
            {
                MessageBox.Show("No such user exists or credentials are invalid.", "No such user", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(user.AccountType != AccountType.Admin)
            {
                MessageBox.Show("You are not authorized to use this application.", "Unathorized attempt to log in", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #region Make form movable
        // Because the form's border style is set to none,
        // it makes it impossible to drag the form by the caption bar.
        // The code below fixes the issue.
        // Reference:  https://stackoverflow.com/questions/1592876/make-a-borderless-form-movable
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        #endregion

        #region Form shadow
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                var cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion
    }
}
