using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.PostulanteModel;

namespace Domain
{
    public class PostulanteModel
    {
        
        UserDao userDao = new UserDao();
        //EDITADO------------------------------------
        //atributos
        private string idPostulante;
        private string nombre;
        private string apePaterno;
        private string apeMaterno;
        private int especialidad;
        private string respuesta;
        private string condicion;
        private int puntaje;
        private int usuario;
        public PostulanteModel(string idPostulante, string nombre, string apePaterno, string apeMaterno, int especialidad, string respuesta, string condicion, int puntaje, int usuario)
        {
            this.IdPostulante = idPostulante;
            this.Nombre = nombre;
            this.ApePaterno = apePaterno;
            this.ApeMaterno = apeMaterno;
            this.Especialidad = especialidad;
            this.Respuesta = respuesta;
            this.Condicion = condicion;
            this.Puntaje = puntaje;
            this.Usuario = usuario;
        }

        public string IdPostulante { get => idPostulante; set => idPostulante = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApePaterno { get => apePaterno; set => apePaterno = value; }
        public string ApeMaterno { get => apeMaterno; set => apeMaterno = value; }
        public int Especialidad { get => especialidad; set => especialidad = value; }
        public string Respuesta { get => respuesta; set => respuesta = value; }
        public string Condicion { get => condicion; set => condicion = value; }
        public int Puntaje { get => puntaje; set => puntaje = value; }
        public int Usuario { get => usuario; set => usuario = value; }

        public PostulanteModel()
        {

        }

        public void generate(string idPostulante, string nombre, string apePaterno, string apeMaterno, int especialidad, string respuesta, string condicion, int puntaje)
        {
            userDao.generate(idPostulante, nombre, apePaterno, apeMaterno, especialidad, respuesta, condicion, puntaje);
        }
    }
}
