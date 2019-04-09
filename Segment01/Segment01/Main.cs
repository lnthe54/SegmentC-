using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Segment01
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            string defaultUsername = "lnthe";
            string defaultPassword = "123";

            string username = tbUsername.Text.Trim();
            string password = tbPassword.Text.Trim();

            if (String.Compare(defaultUsername, username) == 0 
                && String.Compare(defaultPassword, password) == 0)
            {
                Manage manage = new Manage();
                manage.Show();
                this.Hide();
            } else if (username == "" || password == "")
            {
                MessageBox.Show("Bạn phải điền dữ liệu. Vui lòng thử lại !", "Đăng Nhập", MessageBoxButtons.OK);

            }

            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại !", "Đăng Nhập", MessageBoxButtons.OK);
            }
        }

    }
}
