using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Domain;

namespace Presentation
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            initizalizeFields();
        }

        private void initizalizeFields()
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "")
            {
                if (txtPassword.Text != "")
                {
                    UserModel user = new UserModel();
                    var validLogin = user.LoginUser(txtUser.Text , txtPassword.Text);
                    if (validLogin == true)
                    {
                        Home mainmenu = new Home();
                        mainmenu.Show();
                        this.Hide();
                    }
                    else
                    {
                        msgError("Incorrect credential");
                        txtPassword.Clear();
                        txtUser.Clear();
                        txtUser.Focus();
                    }
                }
                else
                {
                    msgError("Please enter Password");
                }
            }
            else
            {
                msgError("Please enter Username");
            }
        }
        private void msgError(string msg)
        {
            lblErrorMessage.Text = "    " + msg;
            lblErrorMessage.Visible = true;
        }
    }
}