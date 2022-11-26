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

namespace Presentation
{
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("server=LAPTOP-8LNIGLG0 ; database=prueba ; integrated security = true");

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
                lista[i].Items.Insert(0, "A");
                lista[i].Items.Insert(1, "B");
                lista[i].Items.Insert(2, "C");
                lista[i].Items.Insert(3, "D");
                lista[i].Items.Insert(4, "E");
                lista[i].Items.Insert(5, "-");
            }
            //cargarOp();
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


            //TENGO EL NUMERO DE ESPECIALIDADES SELECCIONADAS EN LA FACULTAD
            connection.Open();
            SqlCommand consultaSQL2 = new SqlCommand($"SELECT count(NombreEspecialidad) FROM ESPECIALIDAD WHERE FACULTAD = {cbxFacultad.SelectedIndex + 1}", connection);
            //MessageBox.Show((cbxFacultad.SelectedIndex + 1).ToString());
            SqlDataReader reader2 = consultaSQL2.ExecuteReader();
            while (reader2.Read())
            {
                numRes = reader2.GetInt32(0);
            }
            reader2.Close();
            connection.Close();
            //MessageBox.Show(numRes.ToString());


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
            for(int i=0; i<numRes; i++)
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

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            List<System.Windows.Forms.ComboBox> lista = new List<System.Windows.Forms.ComboBox>() { cb_1, cb_2, cb_3, cb_4, cb_5, cb_6, cb_7, cb_8, cb_9,
                cb_10, cb_11, cb_12, cb_13, cb_14, cb_15, cb_16, cb_17, cb_18, cb_19, cb_20, cb_21, cb_22, cb_23, cb_24, cb_25, cb_26, cb_27, cb_28, cb_29,
                cb_30, cb_31, cb_32, cb_33, cb_34, cb_35, cb_36, cb_37, cb_38, cb_39, cb_40, cb_41, cb_42, cb_43, cb_44, cb_45, cb_46, cb_47, cb_48, cb_49,
                cb_50, cb_51, cb_52, cb_53, cb_54, cb_55, cb_56, cb_57, cb_58, cb_59, cb_60, cb_61, cb_62, cb_63, cb_64, cb_65, cb_66, cb_67, cb_68, cb_69,
                cb_70, cb_71, cb_72, cb_73, cb_74, cb_75, cb_76, cb_77, cb_78, cb_79, cb_80, cb_81, cb_82, cb_83, cb_84, cb_85, cb_86, cb_87, cb_88, cb_89,
                cb_90, cb_91, cb_92, cb_93, cb_94, cb_95, cb_96, cb_97, cb_98, cb_99, cb_100};

            for (int i=0; i<100; i++)
            {
                
                lista[i].SelectedIndex = r.Next(0,6);
                
            }

        }

        private void btnCalificar_Click(object sender, EventArgs e)
        {

        }
    }
}
