using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
        }

        private void Manual_Load(object sender, EventArgs e)
        {

        }

        private void combobox_facultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            combobox_carrera.Enabled = true;

            // A)AREA DE CIENCIAS DE LA SALUD
            //Facultad de BIologia
            DataTable Ciencias_Biologicas = new DataTable();
            Ciencias_Biologicas.Columns.Add("Carrera");
            DataRow row = Ciencias_Biologicas.NewRow();
            row["Carrera"] = "Carrera Profesional de Biologia";
            Ciencias_Biologicas.Rows.Add(row);


            //Facultad de Enfemeria
            DataTable Enfermeria = new DataTable();
            Enfermeria.Columns.Add("Carrera");
            DataRow row1 = Enfermeria.NewRow();
            row1["Carrera"] = "Carrera Profesional de Enfermeria";
            Enfermeria.Rows.Add(row1);

            //Facultad de Farmacia y Bioquimica
            DataTable FarmaciayQuimica = new DataTable();
            FarmaciayQuimica.Columns.Add("Carrera");
            DataRow row2 = FarmaciayQuimica.NewRow();
            row2["Carrera"] = "Carrera Profesional de Farmacia y Bioquimica";
            FarmaciayQuimica.Rows.Add(row2);

            //Facultad de Medicina Veterinaria y Zootecnia
            DataTable VeterinariayZoo = new DataTable();
            VeterinariayZoo.Columns.Add("Carrera");
            DataRow row3 = VeterinariayZoo.NewRow();
            row3["Carrera"] = "Carrera Profesional de Medicina Veterinaria y Zootecnia";
            VeterinariayZoo.Rows.Add(row3);

            //Facultad de Obstetricia   
            DataTable Obstetricia = new DataTable();
            Obstetricia.Columns.Add("Carrera");
            DataRow row4 = Obstetricia.NewRow();
            row4["Carrera"] = "Carrera Profesional de Obstetricia";
            Obstetricia.Rows.Add(row4);

            //Facultad de Odontologia
            DataTable Odontologia = new DataTable();
            Odontologia.Columns.Add("Carrera");
            DataRow row5 = Odontologia.NewRow();
            row5["Carrera"] = "Carrera Profesional de Odontologia";
            Odontologia.Rows.Add(row5);

            //Facultad de Psicologia
            DataTable Psicologia = new DataTable();
            Psicologia.Columns.Add("Carrera");
            DataRow row6 = Psicologia.NewRow();
            row6["Carrera"] = "Carrera Profesional de Psicologia";
            Psicologia.Rows.Add(row6);


            // B ) AREA DE CIENCIAS SOCIALES Y HUMANIDADES  //
            //Facultad de Administracion
            DataTable Administracion = new DataTable();
            Administracion.Columns.Add("Carrera");
            DataRow row7 = Administracion.NewRow();
            row7["Carrera"] = "Carrera Profesional de Administracion";
            Administracion.Rows.Add(row7);

            //Facultad de Ciencias de la Comunicacion, Turismo y Arqueologia
            DataTable C_T_A = new DataTable();
            C_T_A.Columns.Add("Carrera");
            DataRow row8 = C_T_A.NewRow();
            row8["Carrera"] = "Carrera Profesional de Comunicacion";
            C_T_A.Rows.Add(row8);
            DataRow row9 = C_T_A.NewRow();
            row9["Carrera"] = "Carrera Profesional de Turismo";
            C_T_A.Rows.Add(row9);
            DataRow row10 = C_T_A.NewRow();
            row10["Carrera"] = "Carrera Profesional de Arqueologia";
            C_T_A.Rows.Add(row10);

            //Facultad de Contabilidad                          FALTA LLAMARLOS PARA IMPRIMIR LOS RESULTADOS
            DataTable Contabilidad = new DataTable();
            Contabilidad.Columns.Add("Carrera");
            DataRow row11 = Contabilidad.NewRow();
            row11["Carrera"] = "Carrera Profesional de Contabilidad";
            Contabilidad.Rows.Add(row11);

            //Facultad de Derecho y Ciencia Politica
            DataTable DYC = new DataTable();
            DYC.Columns.Add("Carrera");
            DataRow row12 = DYC.NewRow();
            row12["Carrera"] = "Carrera Profesional de Derecho";
            DYC.Rows.Add(row12);

            //Facultad de Ciencias de la Educacion y Humanidades
            DataTable Educacion = new DataTable();
            Educacion.Columns.Add("Carrera");
            DataRow row13 = Educacion.NewRow();
            row13["Carrera"] = "Edu. en Lenguaje y literatura";
            Educacion.Rows.Add(row13);
            DataRow row14 = Educacion.NewRow();
            row14["Carrera"] = "Edu. en Historia y Geografia";
            Educacion.Rows.Add(row14);
            DataRow row15 = Educacion.NewRow();
            row15["Carrera"] = "Edu. en Filosofia,Psicologia y Ciencias Sociales";
            Educacion.Rows.Add(row15);
            DataRow row16 = Educacion.NewRow();
            row16["Carrera"] = "Edu. en Ciencias Biologicas y Quimica";
            Educacion.Rows.Add(row16);
            DataRow row17 = Educacion.NewRow();
            row17["Carrera"] = "Edu. en Matematica e informatica";
            Educacion.Rows.Add(row17);
            DataRow row18 = Educacion.NewRow();
            row18["Carrera"] = "Educacion Inicial";
            Educacion.Rows.Add(row18);
            DataRow row19 = Educacion.NewRow();
            row19["Carrera"] = "Educacion Primaria";
            Educacion.Rows.Add(row19);
            DataRow row20 = Educacion.NewRow();
            row20["Carrera"] = "Educacion Aristica";
            Educacion.Rows.Add(row20);
            DataRow row21 = Educacion.NewRow();
            row21["Carrera"] = "Educacion Fisica";
            Educacion.Rows.Add(row21);


            //Facultad de Ciencias Economicas y Negocios Internacionales
            DataTable EconomiayNegocios = new DataTable();
            EconomiayNegocios.Columns.Add("Carrera");
            DataRow row22 = EconomiayNegocios.NewRow();
            row22["Carrera"] = "Carrera Profesional de Economia";
            EconomiayNegocios.Rows.Add(row22);
            DataRow row23 = EconomiayNegocios.NewRow();
            row23["Carrera"] = "Carrera Profesional de Negocios Internacionales";
            EconomiayNegocios.Rows.Add(row23);

            // C ) AREA DE CIENCIAS E INGENIERIA //////////////////////
            //Facultad de AGRONOMIA
            DataTable Agronomia = new DataTable();
            Agronomia.Columns.Add("Carrera");
            DataRow row24 = Agronomia.NewRow();
            row24["Carrera"] = "Carrera Profesional de Agronomia";
            Agronomia.Rows.Add(row24);

            //Facultad de arquitectura
            DataTable arquitectura = new DataTable();
            arquitectura.Columns.Add("Carrera");
            DataRow row25 = arquitectura.NewRow();
            row25["Carrera"] = "Carrera Profesional de arquitectura";
            arquitectura.Rows.Add(row25);

            //Facultad de ciencias
            DataTable ciencias = new DataTable();
            ciencias.Columns.Add("Carrera");
            DataRow row26 = ciencias.NewRow();
            row26["Carrera"] = "Carrera Profesional de Matematica e informatica";
            ciencias.Rows.Add(row26);
            DataRow row27 = ciencias.NewRow();
            row27["Carrera"] = "Carrera Profesional de Fisica";
            ciencias.Rows.Add(row27);
            DataRow row28 = ciencias.NewRow();
            row28["Carrera"] = "Carrera Profesional de Estadistica";
            ciencias.Rows.Add(row28);



            //Facultad de Ingenieria Ambiental y Sanitaria
            DataTable IngAmbientalYSanitaria = new DataTable();
            IngAmbientalYSanitaria.Columns.Add("Carrera");
            DataRow row29= IngAmbientalYSanitaria.NewRow();
            row29["Carrera"] = "Carrera Profesional de Ingenieria Ambiental y Sanitaria";
            IngAmbientalYSanitaria.Rows.Add(row29);

            //Facultad de Ingenieria Civil
            DataTable IngCivil = new DataTable();
            IngCivil.Columns.Add("Carrera");
            DataRow row30 = IngCivil.NewRow();
            row30["Carrera"] = "Carrera Profesional de Ingenieria Civil";
            IngCivil.Rows.Add(row30);

            //Facultad de Ingenieria Mecanica Electronica y Electronica
            DataTable FIMEE = new DataTable();
            FIMEE.Columns.Add("Carrera");
            DataRow row31 = FIMEE.NewRow();
            row31["Carrera"] = "Carrera Profesional de Mecanica Electrica";
            FIMEE.Rows.Add(row31);
            DataRow row32 = FIMEE.NewRow();
            row32["Carrera"] = "Carrera Profesional de Electronica";
            FIMEE.Rows.Add(row32);

            //Facultad de Ingenieria de Minas y Metalurgia
            DataTable FIMM = new DataTable();
            FIMM.Columns.Add("Carrera");
            DataRow row33 = FIMM.NewRow();
            row33["Carrera"] = "Carrera Profesional Ingenieria Minas";
            FIMM.Rows.Add(row33);
            DataRow row34 = FIMM.NewRow();
            row34["Carrera"] = "Carrera Profesional Ingenieria Metalurgia";
            FIMM.Rows.Add(row34);

            //Facultad de Ingenieria Pesquera y de Alimentos
            DataTable FIPA = new DataTable();
            FIPA.Columns.Add("Carrera");
            DataRow row35 = FIPA.NewRow();
            row35["Carrera"] = "Carrera Profesional de Ingenieria Pesquera";
            FIPA.Rows.Add(row35);
            DataRow row36 = FIPA.NewRow();
            row36["Carrera"] = "Carrera Profesional de Ingenieria de Alimentos";
            FIPA.Rows.Add(row36);

            //Facultad de Ingenieria Quimica y Petroquimica
            DataTable FIQP = new DataTable();
            FIQP.Columns.Add("Carrera");
            DataRow row37 = FIQP.NewRow();
            row37["Carrera"] = "Carrera Profesional de Ingenieria Quimica";
            FIQP.Rows.Add(row37);

            //Facultad de Ingenieria de Sistemas
            DataTable FIS = new DataTable();
            FIS.Columns.Add("Carrera");
            DataRow row38 = FIS.NewRow();
            row38["Carrera"] = "Carrera Profesional de Ingenieria de Sistemas";
            FIS.Rows.Add(row38);



            //////////////////////////////////////////////////////////////////////////// A) IMPRESION DE DATOS
            if (combobox_facultad.SelectedIndex == 0)
            {
                combobox_carrera.DataSource = Ciencias_Biologicas;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 1)
            {
                combobox_carrera.DataSource = Enfermeria;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 2)
            {
                combobox_carrera.DataSource = FarmaciayQuimica;
                combobox_carrera.DisplayMember = "Carrera";
            }

            else if (combobox_facultad.SelectedIndex == 3)
            {
                combobox_carrera.DataSource = VeterinariayZoo;
                combobox_carrera.DisplayMember = "Carrera";
            }

            else if (combobox_facultad.SelectedIndex == 4)
            {
                combobox_carrera.DataSource = Obstetricia;
                combobox_carrera.DisplayMember = "Carrera";
            }

            else if (combobox_facultad.SelectedIndex == 5)
            {
                combobox_carrera.DataSource = Odontologia;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 6)
            {
                combobox_carrera.DataSource = Psicologia;
                combobox_carrera.DisplayMember = "Carrera";
            }
            //////////////////////////////////////////////////////////////////////////// B)
            else if (combobox_facultad.SelectedIndex == 7)
            {
                combobox_carrera.DataSource = Administracion;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 8)
            {
                combobox_carrera.DataSource = C_T_A;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 9)
            {
                combobox_carrera.DataSource = Contabilidad;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 10)
            {
                combobox_carrera.DataSource = DYC;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 11)
            {
                combobox_carrera.DataSource = Educacion;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 12)
            {
                combobox_carrera.DataSource = EconomiayNegocios;
                combobox_carrera.DisplayMember = "Carrera";
            }





            //////////////////////////////////////////////////////////////////////////// C)

            else if (combobox_facultad.SelectedIndex == 13)
            {
                combobox_carrera.DataSource = Agronomia;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 14)
            {
                combobox_carrera.DataSource = arquitectura;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 15)
            {
                combobox_carrera.DataSource = ciencias;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 16)
            {
                combobox_carrera.DataSource = IngAmbientalYSanitaria;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 17)
            {
                combobox_carrera.DataSource = IngCivil;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 18)
            {
                combobox_carrera.DataSource = FIMEE;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 19)
            {
                combobox_carrera.DataSource = FIMM;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 20)
            {
                combobox_carrera.DataSource = FIPA;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 21)
            {
                combobox_carrera.DataSource = FIQP;
                combobox_carrera.DisplayMember = "Carrera";
            }
            else if (combobox_facultad.SelectedIndex == 22)
            {
                combobox_carrera.DataSource = FIS;
                combobox_carrera.DisplayMember = "Carrera";
            }
            


        }
    }
}
