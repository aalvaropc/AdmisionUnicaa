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

namespace Presentation
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            timer1.Enabled = true;

        }

        




        private Form activeform;
       
        private void BtnDesplazar_Click(object sender, EventArgs e)
        {
            //i = 1;
            if (MVertical.Width == 214)
            {
                MVertical.Width = 80;
            }
            else
                MVertical.Width = 214;

          
        }
        //public void refrescar(string nuevoNombre)
        //{
        //    nameUser.Text = nuevoNombre;
        //}
        private void OpenChildForm(Form childform, object btnsender)
        {

            if (activeform != null)
            {
                ActiveForm.Activate();
            }
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.Panel_Contenido.Controls.Add(childform);
            this.Panel_Contenido.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }
      
        public void LoadUserData()
        {
            nameUser.Text = UserLoginCache.FirstName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Postulantes(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Manual(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Generar(), sender);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Tiempo.Text = DateTime.Now.ToString();
        }
        int i = 0;
        
        private void Home_Load(object sender, EventArgs e)
        {
            i = Freshh.fresh;
            LoadUserData();
            if (i > 0)
            {
                LoadUserData();
            }
            OpenChildForm(new Postulantes(), sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de cerrar la aplicacion?", "WARNING",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Application.Exit();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Edit(), sender);
        }


        Edit edit = new Edit();
    }
}