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

namespace Presentation
{
    public partial class Postulantes : Form
    {
        public Postulantes()
        {
            InitializeComponent();
        }
        private void Postulantes_Load(object sender, EventArgs e)
        {
            llenarDGV();
            cbxFiltro.Items.Insert(0, "CODIGO");
            cbxFiltro.Items.Insert(1, "NOMBRE");
            cbxFiltro.Items.Insert(2, "ESPECIALIDAD");
            cbxFiltro.Items.Insert(3, "CONDICION");
        }
        SqlConnection connection = new SqlConnection("server=LAPTOP-8LNIGLG0 ; database=prueba ; integrated security = true");
        public void llenarDGV()
        {
            String consulta = "SELECT Postulante.IdPostulante AS CODIGO, CONCAT_WS(' ', Postulante.ApePaterno, Postulante.ApeMaterno, Postulante.Nombre) AS NOMBRE, Especialidad.NombreEspecialidad AS ESPECIALIDAD, Postulante.Condicion AS CONDICION, Puntaje.puntaje AS PUNTAJE from Postulante INNER JOIN Especialidad on Postulante.Especialidad = Especialidad.IdEspecialidad INNER JOIN Puntaje on Puntaje.IdPuntaje = Postulante.Puntaje";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, connection);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;

            //conexion.Open();

            //string consultaNxT = "SELECT COUNT(*) From POSTULANTES WHERE TEMA = 'D' OR TEMA = 'E' OR TEMA = 'F'";
            //SqlCommand nxt = new SqlCommand(consultaNxT, conexion);
            //int numperoPosTema = Convert.ToInt32(nxt.ExecuteScalar());

            //conexion.Close();

            //dataGridView1.Columns.Add("", "CONDICION");
            //for (int i = 0; i < numperoPosTema; i++)
            //{
            //    dataGridView1.Rows[i].Cells[4].Value = i < 20 ? "INGRESO" : "NO INGRESO";
            //}
            dataGridView1.Columns[0].Width = 65;
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 65;
        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
