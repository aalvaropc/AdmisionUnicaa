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
        
        SqlConnection connection = new SqlConnection("server=DESKTOP-8NTIIEU ; database=prueba ; integrated security = true");
        
        private void Postulantes_Load(object sender, EventArgs e)
        {

            llenarDGV();
            insertarOP();
            computoGeneral();
            numeroPostulantes();
            insertarFacultades();
        }
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
            //for (var i = 0; i < dataGridView1.Columns.Count; i++)
            //{
            //    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //}
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 215;
            dataGridView1.Columns[2].Width = 215;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 65;
        }
        public void insertarOP()
        {
            cbxFiltro.Items.Insert(0, "CODIGO");
            cbxFiltro.Items.Insert(1, "NOMBRE");
            cbxFiltro.Items.Insert(2, "APELLIDO PATERNO");
            cbxFiltro.Items.Insert(3, "APELLIDO MATERNO");
            cbxFiltro.Items.Insert(4, "CONDICION");
            cbxFiltro.Items.Insert(5, "ESPECIALIDAD");
        }
        public void insertarFacultades()
        {
            string facultades = "";

            for (int i = 1; i <= 40; i++)
            {
                connection.Open();

                SqlCommand consultaSQL = new SqlCommand($"SELECT NombreEspecialidad FROM Especialidad WHERE IdEspecialidad = '{i}'", connection);

                SqlDataReader reader = consultaSQL.ExecuteReader();
                while (reader.Read())
                {
                    facultades = reader.GetString(0);
                }
                cbxEspecialidad.Items.Insert(i - 1, facultades.Replace("FACULTAD DE", ""));
                reader.Close();
                connection.Close();
            }
        }
        public void computoGeneral()
        {
            connection.Open();
            SqlCommand consultaSQL = new SqlCommand($"SELECT CONCAT_WS(' ', Postulante.ApePaterno, Postulante.ApeMaterno, Postulante.Nombre) FROM Postulante INNER JOIN Puntaje ON Postulante.Puntaje = Puntaje.IdPuntaje WHERE Puntaje.puntaje = (SELECT MAX(puntaje) FROM Puntaje)", connection);
            SqlDataReader reader = consultaSQL.ExecuteReader();
            while (reader.Read())
            {
                txtComputo.Text = reader.GetString(0);
            }
            reader.Close();
            connection.Close();
        }
        public void verSol(string codigo)
        {
            string respuesta = "", sol = "";

            connection.Open();
            SqlCommand consultaSQL1 = new SqlCommand($"SELECT Respuestas FROM Postulante where IdPostulante = {codigo}", connection);
            SqlDataReader reader1 = consultaSQL1.ExecuteReader();
            while (reader1.Read())
            {
                respuesta = reader1.GetString(0);
            }
            reader1.Close();
            connection.Close();
            MessageBox.Show(respuesta.ToString());

            connection.Open();
            SqlCommand consultaSQL2 = new SqlCommand($"select Solucionario.Solucion from Solucionario INNER JOIN Postulante on Postulante.Tema = Solucionario.IdSolucionario where Postulante.IdPostulante={codigo}", connection);
            SqlDataReader reader2 = consultaSQL2.ExecuteReader();
            while (reader2.Read())
            {
                sol = reader2.GetString(0);
            }
            reader2.Close();
            connection.Close();


            for (int i = 0; i < 100; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = (i + 1).ToString();
                dataGridView2.Rows[i].Cells[1].Value = respuesta.Substring(i, 1);
                dataGridView2.Rows[i].Cells[2].Value = sol.Substring(i, 1);
            }
        }
        public void numeroPostulantes()
        {
            connection.Open();
            SqlCommand consultaSQL = new SqlCommand($"SELECT COUNT(Postulante.Nombre) from Postulante", connection);
            SqlDataReader reader = consultaSQL.ExecuteReader();
            while (reader.Read())
            {
                txtPostulantes.Text = reader.GetInt32(0).ToString();
            }
            reader.Close();
            connection.Close();
        }
        public void filtrar(string criterio, string campo)
        {
            string especialidad = "";
            if (cbxEspecialidad.SelectedIndex != -1)
            {
                especialidad = cbxEspecialidad.SelectedItem.ToString();
            }
            if (campo != "Especialidad")
            {
                if (especialidad == "")
                {
                    string consulta = $"SELECT Postulante.IdPostulante AS CODIGO, CONCAT_WS(' ', Postulante.ApePaterno, Postulante.ApeMaterno, Postulante.Nombre) AS NOMBRE, Especialidad.NombreEspecialidad AS ESPECIALIDAD, Postulante.Condicion AS CONDICION, Puntaje.puntaje AS PUNTAJE from Postulante INNER JOIN Especialidad on Postulante.Especialidad = Especialidad.IdEspecialidad INNER JOIN Puntaje on Puntaje.IdPuntaje = Postulante.Puntaje where {campo} like '%{criterio}%'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, connection);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    string consulta = $"SELECT Postulante.IdPostulante AS CODIGO, CONCAT_WS(' ', Postulante.ApePaterno, Postulante.ApeMaterno, Postulante.Nombre) AS NOMBRE, Especialidad.NombreEspecialidad AS ESPECIALIDAD, Postulante.Condicion AS CONDICION, Puntaje.puntaje AS PUNTAJE from Postulante INNER JOIN Especialidad on Postulante.Especialidad = Especialidad.IdEspecialidad INNER JOIN Puntaje on Puntaje.IdPuntaje = Postulante.Puntaje where {campo} like '%{criterio}%' and Especialidad.NombreEspecialidad = '{especialidad}'";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, connection);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            else
            {
                string consulta = $"SELECT Postulante.IdPostulante AS CODIGO, CONCAT_WS(' ', Postulante.ApePaterno, Postulante.ApeMaterno, Postulante.Nombre) AS NOMBRE, Especialidad.NombreEspecialidad AS ESPECIALIDAD, Postulante.Condicion AS CONDICION, Puntaje.puntaje AS PUNTAJE from Postulante INNER JOIN Especialidad on Postulante.Especialidad = Especialidad.IdEspecialidad INNER JOIN Puntaje on Puntaje.IdPuntaje = Postulante.Puntaje where Especialidad.NombreEspecialidad = '{especialidad}'";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, connection);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string campo = "";
            switch (cbxFiltro.SelectedIndex)
            {
                case 0: campo = "IdPostulante"; 
                        
                    break;
                case 1: campo = "Nombre"; break;
                case 2: campo = "ApePaterno"; break;
                case 3: campo = "ApeMaterno"; break;
                case 4: campo = "Condicion"; break;
                case 5: campo = "Especialidad"; break;
            }
            
            filtrar(txtBuscar.Text, campo);
        }

        private void chart2_Click(object sender, EventArgs e)
        {
        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            connection.Open();
            try
            {
                int n = e.RowIndex;
                string codigo = "";


                if (n != -1)
                {
                    codigo = dataGridView1.Rows[n].Cells[0].Value.ToString();

                }

                //textBox1.Text = codigo.ToString();
                String circuloB = $" select blanco from Puntaje where IdPuntaje = 2{codigo.ToString()}";
                String circuloI = $" select incorrecto from Puntaje where IdPuntaje = 2{codigo.ToString()}";
                String circuloC = $" select correcto from Puntaje where IdPuntaje = 2{codigo.ToString()}";

                SqlCommand cB = new SqlCommand(circuloB, connection);
                SqlCommand cC = new SqlCommand(circuloC, connection);
                SqlCommand cI = new SqlCommand(circuloI, connection);

                string circuloBlanco = Convert.ToString(cB.ExecuteScalar());
                string circucloCorrecto = Convert.ToString(cC.ExecuteScalar());
                string circuloIncorrecto = Convert.ToString(cI.ExecuteScalar());


                chart2.Series["Series1"].Points.Clear();
                chart2.Series["Series1"].Points.AddXY("CORRECTO", Convert.ToInt32(circucloCorrecto));
                chart2.Series["Series1"].Points.AddXY("BLANCO", Convert.ToInt32(circuloBlanco));
                chart2.Series["Series1"].Points.AddXY("INCORRECTO", Convert.ToInt32(circuloIncorrecto));
                //verSol(codigo);




                String consultaSQL1 = $"SELECT Respuestas FROM Postulante where IdPostulante = {codigo}";
                SqlCommand connn = new SqlCommand(consultaSQL1, connection);
                string pos = Convert.ToString(connn.ExecuteScalar());



                String consultaSQL2 = $"select Solucionario.Solucion from Solucionario INNER JOIN Postulante on Postulante.Tema = Solucionario.IdSolucionario where Postulante.IdPostulante={codigo}";
                SqlCommand connn2 = new SqlCommand(consultaSQL2, connection);
                string pos2 = Convert.ToString(connn2.ExecuteScalar());


                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Add("npregunta", "PREGUNTA");
                dataGridView2.Columns.Add("postulante", "RESP");
                dataGridView2.Columns.Add("machote", $"SOL");
                dataGridView2.Rows.Add(100);

                dataGridView2.Columns[0].Width = 73;
                dataGridView2.Columns[1].Width = 63;
                dataGridView2.Columns[2].Width = 60;



                for (int i = 0; i < 100; i++)
                {
                    dataGridView2.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dataGridView2.Rows[i].Cells[1].Value = pos.Substring(i, 1);
                    dataGridView2.Rows[i].Cells[2].Value = pos2.Substring(i, 1);
                }


            }
            catch (Exception ex)
            {
            }
            connection.Close();
        }

        private void txtComputo_TextChanged(object sender, EventArgs e)
        {
        }
        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxEspecialidad.SelectedIndex = -1;
            if(cbxFiltro.SelectedIndex == 5)
            {
                txtBuscar.Enabled = false;
            }
            else
            {
                txtBuscar.Enabled = true;
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {
            string consulta = $"SELECT Postulante.IdPostulante AS CODIGO, CONCAT_WS(' ', Postulante.ApePaterno, Postulante.ApeMaterno, Postulante.Nombre) AS NOMBRE, Especialidad.NombreEspecialidad AS ESPECIALIDAD, Postulante.Condicion AS CONDICION, Puntaje.puntaje AS PUNTAJE from Postulante INNER JOIN Especialidad on Postulante.Especialidad = Especialidad.IdEspecialidad INNER JOIN Puntaje on Puntaje.IdPuntaje = Postulante.Puntaje";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, connection);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            cbxEspecialidad.SelectedIndex = -1;
            cbxFiltro.SelectedIndex = -1;
            txtBuscar.Clear();
        }
        private void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            GenerarPDF pdf = new GenerarPDF();
            pdf.Generar();
        }
      

    }
}
