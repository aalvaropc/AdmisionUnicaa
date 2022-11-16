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

        public PostulanteModel(string idPostulante, string nombre, string apePaterno, string apeMaterno)
        {
            this.IdPostulante = idPostulante;
            this.Nombre = nombre;
            this.ApePaterno = apePaterno;
            this.ApeMaterno = apeMaterno;
        }

        public string IdPostulante { get => idPostulante; set => idPostulante = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApePaterno { get => apePaterno; set => apePaterno = value; }
        public string ApeMaterno { get => apeMaterno; set => apeMaterno = value; }

        public PostulanteModel()
        {

        }

        public void generate(string idPostulante, string nombre, string apePaterno, string apeMaterno)
        {
            userDao.generate(idPostulante, nombre, apePaterno, apeMaterno);
        }
    }
}
