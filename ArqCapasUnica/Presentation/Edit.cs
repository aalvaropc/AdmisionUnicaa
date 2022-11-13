using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Cache;
using Domain;

namespace Presentation
{
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            loadUserData();
            initializePassEditControls();
        }

        private void loadUserData()
        {
            //VIEW
            lblUser.Text = UserLoginCache.UserName;
            lblFirstName.Text = UserLoginCache.FirstName;
            lblLastName.Text = UserLoginCache.LastName;
            lblEmail.Text = UserLoginCache.Email;

            //EDIT PANEL
            txtUsername.Text = UserLoginCache.UserName;
            txtFirstName.Text = UserLoginCache.FirstName;
            txtLastName.Text = UserLoginCache.LastName;
            txtEmail.Text = UserLoginCache.Email;
            txtPassword.Text = UserLoginCache.Password;
            txtConfirmPass.Text = UserLoginCache.Password;
            txtCurrentPass.Text = "";
        }

        private void initializePassEditControls()
        {
            LinkEditPass.Text = "Edit";
            txtPassword.Enabled = false;
            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPass.Enabled = false;
            txtConfirmPass.UseSystemPasswordChar = true;
            txtCurrentPass.UseSystemPasswordChar = true;
        }

        private void reset()
        {
            loadUserData();
            initializePassEditControls();
            loadUserData();
        }

        private void LinkEditPass_Click(object sender, EventArgs e)
        {
            if (LinkEditPass.Text == "Edit")
            {
                LinkEditPass.Text = "Cancel";
                txtPassword.Enabled = true;
                txtPassword.Text = "";
                txtConfirmPass.Enabled = true;
                txtConfirmPass.Text = "";
            }
            else if (LinkEditPass.Text == "Cancel")
            {
                reset();
            }
        }
        // Delegado
        //public delegate void ResetNameProfile(string mensaje);
        ////Evento
        //public event ResetNameProfile ResetProfile;
        private void btnSave_Click(object sender, EventArgs e)
        {
            //EJECUTANDO EVENTO DE RESET NAME PROFILE
            //this.ResetProfile =new UserLoginCache.FirstName;


            if (txtPassword.Text.Length > 2)
            {
                if (txtPassword.Text == txtConfirmPass.Text)
                {
                    if (txtCurrentPass.Text == UserLoginCache.Password)
                    {
                        var userModel = new UserModel(idUser: UserLoginCache.IdUser,
                            loginName: txtUsername.Text,
                            password: txtPassword.Text,
                            firstName: txtFirstName.Text,
                            lastName: txtLastName.Text,
                            email: txtEmail.Text);
                        var result = userModel.editUserProfile();
                        MessageBox.Show(result);
                        reset();
                        
                    }
                    else
                        MessageBox.Show("Contraseña actual incorrecta", "Try again");
                }
                else
                    MessageBox.Show("La contraseña no coincide", "Try again");
            }
            else
                MessageBox.Show("La contraseña debe tener mas de 2 caracteres");
            
        }

        


    }
}
