using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Common.Cache;
using Domain;
using System.Data.SqlClient;
using Common.Cache;



namespace Presentation
{
    public partial class Generar : Form
    {
        public Generar()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("server=DESKTOP-8NTIIEU ; database=prueba ; integrated security = true");
        private void Generar_Load(object sender, EventArgs e)
        {
           

        }
        int almacenado = 0;
        string consulta = "";
        string tema = "";
        int especialidad = 1;
        int idsol = 0;
        int y = 0;
        //public string resp { get; }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string resp = "";
            almacenado = SaveGenerate.Save;
            if (y != 0)
            {
                dataGridView1.DataSource = null;
            }
            int respIncorrecta = 0;
            int respCorrecta = 0;
            int respBlanco = 0;


            string fullName;
            int cantidadPos = Convert.ToInt32(txtCantAlumnos.Text);
            StreamReader sr = new StreamReader(@"C:\Users\Estilos\Desktop\J\NombresPostulantesValidos.txt");

            Random r = new Random();
            
            string[] op = { "A", "B", "C", "D", "E", "-" };
            double puntaje = 0;
            string gIdPostulante = "";
            int prob = 0;
            for (int i = 1; i <= cantidadPos + almacenado; i++)
            {
                int codigo = r.Next(70000000, 79999999);
                fullName = sr.ReadLine();

                if (i != cantidadPos+almacenado)
                {
                    gIdPostulante += codigo.ToString() + ",";
                }
                else
                {
                    gIdPostulante += codigo.ToString();
                }
                
                string nombre = "";
                string apPaterno = "";
                string apMaterno = "";

                nombre = $"{fullName.Split(' ')[2]} {fullName.Split(' ')[3]}";
                apPaterno = fullName.Split(' ')[0];
                apMaterno = fullName.Split(' ')[1];

                //respuestas
                for (int y = 1; y <= 100; y++)
                {
                    resp += op[r.Next(0, 6)].ToString();
                }


                especialidad = r.Next(1, 41);

                if (especialidad >= 1 && especialidad <= 8)
                {
                    switch (r.Next(0, 3))
                    {
                        case 0: tema = "F"; idsol = 1; break;
                        case 1: tema = "G"; idsol = 2; break;
                        case 2: tema = "H"; idsol = 3; break;
                    }
                }

                if (especialidad >= 9 && especialidad <= 23)
                {
                    switch (r.Next(0, 3))
                    {
                        case 0: tema = "I"; idsol = 4; break;
                        case 1: tema = "J"; idsol = 5; break;
                        case 2: tema = "K"; idsol = 6; break;
                    }
                }

                if (especialidad >= 24 && especialidad <= 40)
                {
                    switch (r.Next(0, 3))
                    {
                        case 0: tema = "L"; idsol = 7; break;
                        case 1: tema = "M"; idsol = 8; break;
                        case 2: tema = "N"; idsol = 9; break;
                    }
                }


                connection.Open();
                SqlCommand consultaSQL = new SqlCommand($"SELECT SOLUCION FROM SOLUCIONARIO WHERE TEMA = '{tema}'", connection);
                SqlDataReader reader = consultaSQL.ExecuteReader();
                while (reader.Read())
                {
                    consulta = reader.GetString(0);
                }
                reader.Close();

                //resp[0] = "hola";
                
                for (int j = 0; j < 100; j++)
                {
                    prob = r.Next(0, 3);
                    if (resp.Substring(j, 1).ToString() != consulta[j].ToString() && prob == 2)
                    {
                        //if(consulta.Substring(j, 1) != null)
                        //{
                        //}
                        //resp.Replace(resp.Substring(j, 1), consulta[j]);
                        StringBuilder sb = new StringBuilder(resp);
                        sb[j] = consulta[j];
                        resp = sb.ToString();
                    }
                    if (resp[j] == '-')
                    {
                        puntaje += 1.25;
                        respBlanco++;
                    }
                    else
                    {
                        if (resp[j] == consulta[j])
                        {
                            puntaje += 20;
                            respCorrecta++;
                        }
                        else
                        {
                            puntaje -= 1.25;
                            respIncorrecta++;
                        }
                    }
                }
                
                SqlCommand comando = new SqlCommand($"INSERT INTO Puntaje VALUES (2{codigo.ToString()}, {respBlanco.ToString()}, {respCorrecta.ToString()}, {respIncorrecta.ToString()}, {(puntaje.ToString()).Replace(",", ".")})", connection);
                comando.ExecuteNonQuery();
                connection.Close();

                if (almacenado == 0)
                {

                    PostulanteModel postulante = new PostulanteModel();
                    postulante.generate(codigo.ToString(), nombre, apPaterno, apMaterno, especialidad, resp, "NO INGRESO", 200000000 + codigo, idsol);
                }
                else
                {
                    if (i >= almacenado + 1 && i <= cantidadPos + almacenado)
                    {
                        PostulanteModel postulante = new PostulanteModel();
                        postulante.generate(codigo.ToString(), nombre, apPaterno, apMaterno, especialidad, resp, "NO INGRESO", 200000000 + codigo, idsol);
                    }
                }
                resp = "";
                respIncorrecta = 0;
                respCorrecta = 0;
                respBlanco = 0;
                puntaje = 0;
            }
            sr.Close();
            SqlConnection cn = new SqlConnection("Server=DESKTOP-8NTIIEU;DataBase=prueba; integrated security=true"); ;
            SqlCommand cmd = new SqlCommand($"Select * from Postulante where IdPostulante in ({gIdPostulante})", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cn.Close();


            almacenado += cantidadPos;
            for (var i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            y++;
            gIdPostulante = "";
            SaveGenerate.Save = almacenado;


            //11-37
            string[] vacantes = { "10", "12", "15", "12", "12", "10", "14", "10", "12", "15", "12", "15", "12", "14", "12", "12", "14", "10", "15", "18", "12", "14", "10", "12", "14", "13", "12", "14", "10", "12", "10", "12", "14", "12", "15", "18", "11", "14", "15", "11" };
            MessageBox.Show(vacantes[especialidad - 1]+ " - "+ especialidad.ToString());

            //CORREGIR UPDATE

            for(int i=0; i<40; i++)
            {
                connection.Open();
                SqlCommand comandoo = new SqlCommand($"update Postulante set Condicion = 'INGRESO' where IdPostulante IN(SELECT TOP {vacantes[i]} IdPostulante FROM Postulante where Postulante.Especialidad = {(especialidad+1).ToString()} order by (select TOP 1 Puntaje.puntaje from Puntaje inner join Postulante on Puntaje.IdPuntaje=Postulante.Puntaje where Postulante.Especialidad={(especialidad+1).ToString()}) desc)", connection);
                comandoo.ExecuteNonQuery();
                connection.Close();
            }
            



        }

    }
}