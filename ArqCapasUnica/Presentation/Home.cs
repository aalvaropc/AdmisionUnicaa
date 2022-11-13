﻿using System;
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
            //TIEMPO JAJAJA DE RELLENO
            timer1.Enabled = true;
        }

        private Form activeform;
        // PARA DESPLAZAR LA BARRITA 
        private void BtnDesplazar_Click(object sender, EventArgs e)
        {

            if (MVertical.Width == 214)
            {
                MVertical.Width = 80;
            }
            else
                MVertical.Width = 214;
        }
        // SORRY NO ENTENDI MUCHO PERO SI ME SALIO, ES UN COPIA Y PEGA DEL LABEL "HOME" QUE ESTA ABAJO DEL LOGO SAN LUCHITO.
        //ADJUNTO VIDEO Y MIN DEL VIDEO XD  : https://www.youtube.com/watch?v=BtOEztT1Qzk , RESULTADO : MIN 20:45, SE MUESTRA COMO LA PALABRA "HOME" CAMBIA RESPECTIVAMENTE AL NOMBRE DEL BOTON CORRESPONDIENTE.
        private void OpenChildForm(Form childform, object btnsender) {

            if(activeform != null){
            ActiveForm.Activate();
            }
            //ActivateButton(btnsender);
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.Panel_Contenido.Controls.Add(childform);
            this.Panel_Contenido.Tag = childform;
            childform.BringToFront();
            childform.Show(); 
            lbltitle.Text = childform.Text;
        }
        private void LoadUserData()
        {
            nameUser.Text = UserLoginCache.FirstName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Para visualizar Los form en el "Panel_Contenido" de Principal 
            OpenChildForm(new Postulantes(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Para visualizar Los form en el "Panel_Contenido" de Principal 
            OpenChildForm(new Manual(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Para visualizar Los form en el "Panel_Contenido" de Principal 
            OpenChildForm(new Generar(), sender);
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    OpenChildForm(new Edit(), sender);

        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            //EL TIEMPO DE RELLENO X2
            Tiempo.Text = DateTime.Now.ToString();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadUserData();

            //Sirve para actualizar el nombre del usuario del home una vez editado
            //edit.ResetProfile += new Edit.ResetNameProfile(ChangeName);
        }
        //sirve para cambiar el nombre una vez editado
        //void ChangeName(string mensaje)
        //{
        //    this.nameUser.Text = mensaje;
        //}
        private void button5_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Estas seguro de cerrar la aplicacion?", "WARNING",
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
