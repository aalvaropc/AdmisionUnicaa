﻿using System;
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



namespace Presentation
{
    public partial class Generar : Form
    {
        public Generar()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("server=LAPTOP-8LNIGLG0 ; database=prueba ; integrated security = true");
        private void Generar_Load(object sender, EventArgs e)
        {
           

        }
        int almacenado = 0;
        string consulta = "";
        string tema = "";
        int especialidad = 1;
        int idsol = 0;
        int y = 0;
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(y.ToString());
            if (y != 0)
            {
                dataGridView1.DataSource = null;
            }
            int respIncorrecta = 0;
            int respCorrecta = 0;
            int respBlanco = 0;


            string fullName;
            int cantidadPos = Convert.ToInt32(txtCantAlumnos.Text);
            StreamReader sr = new StreamReader("C:\\Users\\estilos\\Desktop\\NombresPostulantesValidos.txt");

            Random r = new Random();
            string resp = "";
            string[] op = { "A", "B", "C", "D", "E", "-" };
            double puntaje = 0;
            //List<string> gIdPostulante = new List<string>();
            string gIdPostulante = "";
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
                //Console.WriteLine(fullName

                nombre = $"{fullName.Split(' ')[2]} {fullName.Split(' ')[3]}";
                apPaterno = fullName.Split(' ')[0];
                apMaterno = fullName.Split(' ')[1];

                //respuestas
                for (int y = 1; y <= 100; y++)
                {
                    resp += op[r.Next(0, 6)];
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


                for (int j = 0; j < 100; j++)
                {

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
                    postulante.generate(codigo.ToString(), nombre, apPaterno, apMaterno, especialidad, resp, "INGRESO", 200000000 + codigo, idsol);
                }
                else
                {
                    if (i >= almacenado + 1 && i <= cantidadPos + almacenado)
                    {
                        PostulanteModel postulante = new PostulanteModel();
                        postulante.generate(codigo.ToString(), nombre, apPaterno, apMaterno, especialidad, resp, "INGRESO", 200000000 + codigo, idsol);
                    }
                }
                resp = ""; //resetea las respuestas generadas
                respIncorrecta = 0;
                respCorrecta = 0;
                respBlanco = 0;

            }
            sr.Close();
            //MessageBox.Show(gIdPostulante);
            SqlConnection cn = new SqlConnection("Server=LAPTOP-8LNIGLG0;DataBase=prueba; integrated security=true"); ;
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
            //dataGridView1.Columns[0].Width = 70;
            //dataGridView1.Columns[1].Width = 115;
            //dataGridView1.Columns[2].Width = 80;
            //dataGridView1.Columns[3].Width = 80;
            //dataGridView1.Columns[4].Width = 70;
            //dataGridView1.Columns[5].Width = 700;
            //dataGridView1.Columns[6].Width = 80;
            //dataGridView1.Columns[7].Width = 68;
            //dataGridView1.Columns[8].Width = 50;
            //dataGridView1.Columns[9].Width = 45;
            y++;
            gIdPostulante = "";

        }



    }
}