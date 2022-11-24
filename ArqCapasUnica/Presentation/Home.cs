using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Cache;
using DataAccess;

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

        UserDao userDao = new UserDao();
        private String consulta;
        private Form activeform;
        private SqlDataAdapter adapter; //Para rellenar el datagrid view
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
            TablaPostulantes();
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


        //AGREGADO POR JERALE BC

        //Metodos
        private void TablaPostulantes() 
        {
            adapter = userDao.llenarTabla(0,"");
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")  
            {
                dataGridView1.Columns.Clear();

                adapter = userDao.llenarTabla(1,txtBuscar.Text);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else 
            {
                MessageBox.Show("Cuadro e búsqueda vacío","Error");
            }
        }

        private void cboxArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] cods = {"A","B","C"};

            dataGridView1.Columns.Clear();

            adapter = userDao.llenarTabla(2,cods[cboxArea.SelectedIndex]);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void cboxFacultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] cods = { "AF1", "AF2", "AF3", "AF4", "AF5", "AF6", "AF7", "AF8",
                              "BF1", "BF2", "BF3", "BF4", "BF5", "BF6", "BF7", "BF8", "BF9", "BF10", "BF11", "BF12", "BF13", "BF14", "BF15",
                              "CF1", "CF2", "CF3", "CF4", "CF5", "CF6", "CF7", "CF8", "CF9", "CF10", "CF11", "CF12", "CF13", "CF14", "CF15", "CF16", "CF17",
            };

            dataGridView1.Columns.Clear();

            adapter = userDao.llenarTabla(2,cods[cboxFacultad.SelectedIndex]);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
