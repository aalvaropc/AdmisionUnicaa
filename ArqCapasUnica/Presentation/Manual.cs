using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;
using Domain;

namespace Presentation
{
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("server=LAPTOP-8LNIGLG0 ; database=prueba ; integrated security = true");
        Random r = new Random();
        string tema = "", consulta = "", temag="";
        private void Manual_Load(object sender, EventArgs e)
        {

            List<System.Windows.Forms.ComboBox> lista = new List<System.Windows.Forms.ComboBox>() { cb_1, cb_2, cb_3, cb_4, cb_5, cb_6, cb_7, cb_8, cb_9,
                cb_10, cb_11, cb_12, cb_13, cb_14, cb_15, cb_16, cb_17, cb_18, cb_19, cb_20, cb_21, cb_22, cb_23, cb_24, cb_25, cb_26, cb_27, cb_28, cb_29,
                cb_30, cb_31, cb_32, cb_33, cb_34, cb_35, cb_36, cb_37, cb_38, cb_39, cb_40, cb_41, cb_42, cb_43, cb_44, cb_45, cb_46, cb_47, cb_48, cb_49,
                cb_50, cb_51, cb_52, cb_53, cb_54, cb_55, cb_56, cb_57, cb_58, cb_59, cb_60, cb_61, cb_62, cb_63, cb_64, cb_65, cb_66, cb_67, cb_68, cb_69,
                cb_70, cb_71, cb_72, cb_73, cb_74, cb_75, cb_76, cb_77, cb_78, cb_79, cb_80, cb_81, cb_82, cb_83, cb_84, cb_85, cb_86, cb_87, cb_88, cb_89,
                cb_90, cb_91, cb_92, cb_93, cb_94, cb_95, cb_96, cb_97, cb_98, cb_99, cb_100};

            for (int i = 0; i < 100; i++)
            {
                lista[i].Items.Add("A");
                lista[i].Items.Add("B");
                lista[i].Items.Add("C");
                lista[i].Items.Add("D");
                lista[i].Items.Add("E");
                lista[i].Items.Add("-");
            }
            string facultades = "";

            for (int i = 1; i <= 24; i++)
            {
                connection.Open();

                SqlCommand consultaSQL = new SqlCommand($"SELECT NombreFacultad FROM FACULTAD WHERE IdFacultad = '{i}'", connection);

                SqlDataReader reader = consultaSQL.ExecuteReader();
                while (reader.Read())
                {
                    facultades = reader.GetString(0);
                }
                cbxFacultad.Items.Insert(i - 1, facultades.Replace("FACULTAD DE", ""));
                reader.Close();
                connection.Close();
            }

        }
        string especialidad = "";
        int numRes = 0;
        int fresh = 0;
        int nameArea = 0;

        private void combobox_facultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fresh != 0)
            {
                cbxEspecialidad.Items.Clear();
            }
            fresh++;


            cbxEspecialidad.Enabled = true;

            connection.Open();
            SqlCommand consultaSQL2 = new SqlCommand($"SELECT count(NombreEspecialidad) FROM ESPECIALIDAD WHERE FACULTAD = {cbxFacultad.SelectedIndex + 1}", connection);
            SqlDataReader reader2 = consultaSQL2.ExecuteReader();
            while (reader2.Read())
            {
                numRes = reader2.GetInt32(0);
            }
            reader2.Close();
            connection.Close();
            


            //ITERNO SOBRE LAS ESPECIALIDADES DENTRO DE LA FACULTAD
            connection.Open();
            List<string> invoices = new List<string>();
            SqlCommand consultaSQL = new SqlCommand($"SELECT NombreEspecialidad FROM Especialidad WHERE Facultad = {cbxFacultad.SelectedIndex + 1}", connection);

            SqlDataReader reader = consultaSQL.ExecuteReader();
            while (reader.Read())
            {
                invoices.Add(reader.GetString(0));
            }


            reader.Close();
            connection.Close();
            for (int i = 0; i < numRes; i++)
            {
                cbxEspecialidad.Items.Insert(i, invoices[i].ToString());
            }


            //LISTA NUMERO PREGUNTAS
            List<System.Windows.Forms.Label> preguntas = new List<System.Windows.Forms.Label>() { cant_preg_biologia, cant_preg_quimica, cant_preg_fisica,cant_preg_filosofia, cant_preg_economia,
                cant_preg_geografia,cant_preg_historia_u, cant_preg_historia_p, cant_preg_civica, cant_preg_psicologia, cant_preg_literatura, cant_preg_lenguaje, cant_preg_trigonometria, cant_preg_geometria
            , cant_preg_algebra, cant_preg_aritmetica, cant_preg_r_v, cant_preg_r_m};
            connection.Open();
            SqlCommand consultaSQL3 = new SqlCommand($" SELECT AREA FROM Facultad  WHERE IdFacultad = {cbxFacultad.SelectedIndex + 1}", connection);
            SqlDataReader reader3 = consultaSQL3.ExecuteReader();
            while (reader3.Read())
            {
                nameArea = reader3.GetInt32(0);
            }
            reader3.Close();
            connection.Close();

            int[,] nres = new int[3, 18] { { 8,6,6,2,2,2,2,2,2,3,4,6,3,4,4,4,20,20 }, { 2,2,2,4,4,5,5,5,5,4,6,8,2,2,2,2,20,20 },
                                        { 2,5,8,2,2,2,2,2,2,2,3,4,6,6,6,6,20,20 } };


            for (int i = 0; i < 18; i++)
            {
                preguntas[i].Text = nres[nameArea - 1, i].ToString();
            }
        }


        private void groupbox_tarjeta_omr_Enter(object sender, EventArgs e)
        {

        }

        int respBlanco = 0, respCorrecta = 0, respIncorrecta = 0;



        double puntaje = 0;
        public void limpiar()
        {
            List<System.Windows.Forms.ComboBox> lista = new List<System.Windows.Forms.ComboBox>() { cb_1, cb_2, cb_3, cb_4, cb_5, cb_6, cb_7, cb_8, cb_9,
                cb_10, cb_11, cb_12, cb_13, cb_14, cb_15, cb_16, cb_17, cb_18, cb_19, cb_20, cb_21, cb_22, cb_23, cb_24, cb_25, cb_26, cb_27, cb_28, cb_29,
                cb_30, cb_31, cb_32, cb_33, cb_34, cb_35, cb_36, cb_37, cb_38, cb_39, cb_40, cb_41, cb_42, cb_43, cb_44, cb_45, cb_46, cb_47, cb_48, cb_49,
                cb_50, cb_51, cb_52, cb_53, cb_54, cb_55, cb_56, cb_57, cb_58, cb_59, cb_60, cb_61, cb_62, cb_63, cb_64, cb_65, cb_66, cb_67, cb_68, cb_69,
                cb_70, cb_71, cb_72, cb_73, cb_74, cb_75, cb_76, cb_77, cb_78, cb_79, cb_80, cb_81, cb_82, cb_83, cb_84, cb_85, cb_86, cb_87, cb_88, cb_89,
                cb_90, cb_91, cb_92, cb_93, cb_94, cb_95, cb_96, cb_97, cb_98, cb_99, cb_100};
            txtCodigo.Clear();
            txtNombre.Clear();
            txtApPaterno.Clear();
            txtApMaterno.Clear();
            txtCodigo.Clear();
            cbxEspecialidad.Items.Clear();
            cbxFacultad.SelectedIndex = -1;
            txtCorrecto.Clear();
            txtIncorrecto.Clear();
            txtBlanco.Clear();
            txtNota.Clear();
            for (int i = 0; i < 100; i++)
            {
                lista[i].SelectedIndex = -1;
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
                string nombre = txtNombre.Text;
                string apePat = txtApPaterno.Text;
                string apeMat = txtApMaterno.Text;
                string codigo = txtCodigo.Text;
                string especialidad = cbxEspecialidad.SelectedItem.ToString();
                string correcta = txtCorrecto.ToString();
                string incorrecta = txtIncorrecto.ToString();
                string blanco = txtBlanco.ToString();
                string nota = txtNota.ToString();
                string temaPos = tema;
                string respuesta = resp;

                connection.Open();
                String consultaSQL1 = $"select IdEspecialidad from Especialidad where NombreEspecialidad ='{especialidad}'";
                SqlCommand connn = new SqlCommand(consultaSQL1, connection);
                int codEspecialidad = Convert.ToInt32(connn.ExecuteScalar());
                connection.Close();
                
                connection.Open();
                String consultaSQL2 = $"select IdSolucionario from Solucionario where Tema = '{temaPos}';";
                SqlCommand connn2 = new SqlCommand(consultaSQL2, connection);
                int codSol = Convert.ToInt32(connn2.ExecuteScalar());
                connection.Close();

                connection.Open();
                //INSERTAR PUNTAJE
                //MessageBox.Show($"INSERT INTO Puntaje VALUES (2{codigo}, {blanco}, {correcta}, {incorrecta}, {nota})");
                SqlCommand comando = new SqlCommand($"INSERT INTO Puntaje VALUES (2{codigo}, {respBlanco}, {respCorrecta}, {respIncorrecta}, {(puntaje.ToString()).Replace(",", ".")})", connection);
                comando.ExecuteNonQuery();

                connection.Close();

                //GENERANDO POSTULANTE
                PostulanteModel postulante = new PostulanteModel();
                postulante.generate(codigo, nombre, apePat, apeMat, codEspecialidad, respuesta, "NO INGRESO", 200000000 + Convert.ToInt32(codigo), codSol);

         
                resp = "";
                tema = "";
                
                MessageBox.Show("Registro guardado exitosamente, Sr(a): " + txtNombre.Text, "GUARDANDO ....", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            limpiar();
            respBlanco = 0;
            respCorrecta = 0;
            respIncorrecta = 0;
            puntaje = 0;
        }

        string resp = "";

        public void generadorTema()
        {
            //OBTENGO EL AREA
            connection.Open();
            SqlCommand consultaSQL3 = new SqlCommand($" SELECT AREA FROM Facultad  WHERE IdFacultad = {cbxFacultad.SelectedIndex + 1}", connection);
            SqlDataReader reader3 = consultaSQL3.ExecuteReader();
            while (reader3.Read())
            {
                nameArea = reader3.GetInt32(0);
            }
            reader3.Close();
            connection.Close();

            switch (nameArea)
            {
                case 1:
                    switch (r.Next(0, 3))
                    {
                        case 0: tema += "F"; break;
                        case 1: tema += "G"; break;
                        case 2: tema += "H"; break;
                    }; break;
                case 2:
                    switch (r.Next(0, 3))
                    {
                        case 0: tema += "I"; break;
                        case 1: tema += "J"; break;
                        case 2: tema += "K"; break;
                    }; break;
                case 3:
                    switch (r.Next(0, 3))
                    {
                        case 0: tema += "L"; break;
                        case 1: tema += "M"; break;
                        case 2: tema += "N"; break;
                    }; break;
            }
            temag += tema;
        }



        private void btnCalificar_Click(object sender, EventArgs e)
        {

            List<System.Windows.Forms.ComboBox> lista = new List<System.Windows.Forms.ComboBox>() { cb_1, cb_2, cb_3, cb_4, cb_5, cb_6, cb_7, cb_8, cb_9,
                cb_10, cb_11, cb_12, cb_13, cb_14, cb_15, cb_16, cb_17, cb_18, cb_19, cb_20, cb_21, cb_22, cb_23, cb_24, cb_25, cb_26, cb_27, cb_28, cb_29,
                cb_30, cb_31, cb_32, cb_33, cb_34, cb_35, cb_36, cb_37, cb_38, cb_39, cb_40, cb_41, cb_42, cb_43, cb_44, cb_45, cb_46, cb_47, cb_48, cb_49,
                cb_50, cb_51, cb_52, cb_53, cb_54, cb_55, cb_56, cb_57, cb_58, cb_59, cb_60, cb_61, cb_62, cb_63, cb_64, cb_65, cb_66, cb_67, cb_68, cb_69,
                cb_70, cb_71, cb_72, cb_73, cb_74, cb_75, cb_76, cb_77, cb_78, cb_79, cb_80, cb_81, cb_82, cb_83, cb_84, cb_85, cb_86, cb_87, cb_88, cb_89,
                cb_90, cb_91, cb_92, cb_93, cb_94, cb_95, cb_96, cb_97, cb_98, cb_99, cb_100};

            connection.Open();
            SqlCommand consultaSQL = new SqlCommand($"SELECT SOLUCION FROM SOLUCIONARIO WHERE TEMA = '{tema}'", connection);
            SqlDataReader reader = consultaSQL.ExecuteReader();
            while (reader.Read())
            {
                consulta = reader.GetString(0);
            }
            reader.Close();
            connection.Close();
            for (int j = 0; j < 100; j++)
            {
                resp += lista[j].SelectedItem.ToString();

                if (lista[j].Text == "-")
                {
                    puntaje += 1.25;
                    respBlanco++;
                }
                else
                {
                    if (lista[j].Text == consulta[j].ToString())
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
            //MessageBox.Show(resp);
            txtCorrecto.Text = respCorrecta.ToString();
            txtIncorrecto.Text = respIncorrecta.ToString();
            txtBlanco.Text = respBlanco.ToString();
            txtNota.Text = puntaje.ToString();
        }

        string solucion2 = "";
        int prob=0;
        private void btnRandom_Click(object sender, EventArgs e)
        {
            generadorTema();
            //MessageBox.Show(solucion2.ToString());
            connection.Open();
            SqlCommand consultaSQL = new SqlCommand($"SELECT SOLUCION FROM SOLUCIONARIO WHERE TEMA = '{temag}'", connection);
            SqlDataReader reader = consultaSQL.ExecuteReader();
            while (reader.Read())
            {
                solucion2 = reader.GetString(0);
            }
            reader.Close();
            connection.Close();
            //MessageBox.Show(temag.ToString());
            //MessageBox.Show(solucion2.ToString());

            List<System.Windows.Forms.ComboBox> lista = new List<System.Windows.Forms.ComboBox>() { cb_1, cb_2, cb_3, cb_4, cb_5, cb_6, cb_7, cb_8, cb_9,
                cb_10, cb_11, cb_12, cb_13, cb_14, cb_15, cb_16, cb_17, cb_18, cb_19, cb_20, cb_21, cb_22, cb_23, cb_24, cb_25, cb_26, cb_27, cb_28, cb_29,
                cb_30, cb_31, cb_32, cb_33, cb_34, cb_35, cb_36, cb_37, cb_38, cb_39, cb_40, cb_41, cb_42, cb_43, cb_44, cb_45, cb_46, cb_47, cb_48, cb_49,
                cb_50, cb_51, cb_52, cb_53, cb_54, cb_55, cb_56, cb_57, cb_58, cb_59, cb_60, cb_61, cb_62, cb_63, cb_64, cb_65, cb_66, cb_67, cb_68, cb_69,
                cb_70, cb_71, cb_72, cb_73, cb_74, cb_75, cb_76, cb_77, cb_78, cb_79, cb_80, cb_81, cb_82, cb_83, cb_84, cb_85, cb_86, cb_87, cb_88, cb_89,
                cb_90, cb_91, cb_92, cb_93, cb_94, cb_95, cb_96, cb_97, cb_98, cb_99, cb_100};

            for (int i = 0; i < 100; i++)
            {
                prob = r.Next(0, 4);
                lista[i].SelectedIndex = r.Next(0, 6);

                if (prob == 3 && lista[i].SelectedItem.ToString() != solucion2.Substring(i, 1).ToString())
                {
                    lista[i].SelectedItem = solucion2.Substring(i, 1).ToString();
                }
            }
            temag = "";
        }


    }
}