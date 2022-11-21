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


using System.Data;
using System.Data.SqlClient;

namespace Presentation
{
    public partial class Generar : Form
    {
        public Generar()
        {
            InitializeComponent();
        }

        private void Generar_Load(object sender, EventArgs e)
        {

        }
        int almacenado = 0;
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string fullName;
            int cantidadPos = Convert.ToInt32(txtCantAlumnos.Text);
            StreamReader sr = new StreamReader("C:\\Users\\estilos\\Desktop\\NombresPostulantesValidos.txt");

            //_----------------
            Random r = new Random();
            for (int i = 1; i <= cantidadPos + almacenado; i++)
            {
                fullName = sr.ReadLine();

                string nombre = "";
                string apPaterno = "";
                string apMaterno = "";
                //Console.WriteLine(fullName

                nombre = $"{fullName.Split(' ')[2]} {fullName.Split(' ')[3]}";
                apPaterno = fullName.Split(' ')[0];
                apMaterno = fullName.Split(' ')[1];
                
                
                if (almacenado == 0)
                {
                    //MessageBox.Show(nombre + "-" + apPaterno + "-" + apMaterno);
                    PostulanteModel postulante = new PostulanteModel();
                    postulante.generate(r.Next(70000000, 79999999).ToString(), nombre, apPaterno, apMaterno);
                }
                else
                {
                    if (i >= almacenado + 1 && i <= cantidadPos + almacenado)
                    {
                        PostulanteModel postulante = new PostulanteModel();
                        postulante.generate(r.Next(70000000, 79999999).ToString(), nombre, apPaterno, apMaterno);
                    }
                }
            }

           
            //---------


            SqlConnection cn = new SqlConnection("Server=LAPTOP-8LNIGLG0;DataBase=MyCompany; integrated security=true"); ;
            SqlCommand cmd = new SqlCommand("Select * from Postulante", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cn.Close();




            //---------



            sr.Close();
            almacenado += cantidadPos;

            //---------
            //fullName = sr.ReadLine();
            //while (fullName != null)
            //{
            //    string nombre="";
            //    string apPaterno="";
            //    string apMaterno="";
            //    //Console.WriteLine(fullName);

            //    Random r = new Random();


            //    nombre = $"{fullName.Split(' ')[0].ToString()} {fullName.Split(' ')[1].ToString()}";
            //    apPaterno = fullName.Split(' ')[2].ToString();
            //    apMaterno = fullName.Split(' ')[3].ToString();

            //    PostulanteModel postulante = new PostulanteModel();
            //    postulante.generate(r.Next(70000000, 79999999).ToString(), nombre, apPaterno, apMaterno);


            //    fullName = sr.ReadLine();
            //}
            ////close the file
            //sr.Close();
        }

    }
}
