using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common.Cache;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace DataAccess
{
    public class UserDao : ConnectionToSql
    {
        public void editProfile(int id, string userName, string password, string name, string lastName, string mail)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update Users set " +
                        "loginName=@userName, Password=@pass, FirstName=@name, LastName=@lastName, Email=@mail where UserId=@id";
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@pass", password);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@mail", mail);
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
            
        }        
        public bool Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select *from Users where LoginName=@user and Password=@pass";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserLoginCache.IdUser = reader.GetInt32(0);
                            UserLoginCache.UserName = reader.GetString(1);
                            UserLoginCache.Password = reader.GetString(2);
                            UserLoginCache.FirstName = reader.GetString(3);
                            UserLoginCache.LastName = reader.GetString(4);
                            UserLoginCache.Email = reader.GetString(5);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        int Idespecialidad = 0, especialidadd=0;
        string[] vacantes = { "10", "12", "15", "12", "12", "10", "14", "10", "12", "15", "12", "15", "12", "14", "12", "12", "14", "10", "15", "18", "12", "14", "10", "12", "14" , "13", "12", "14", "10", "12" , "10", "12", "14", "12", "15", "18", "11", "14", "15", "11"};
        public void generate(string idPostulante, string nombre, string apePaterno, string apeMaterno, int especialidad, string respuestas, string condicion, int puntaje, int tema)
        {
            using (var connection = GetConnection())
            {
                especialidadd = especialidad;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"Insert into Postulante values('{idPostulante.ToString()}', '{nombre.ToString()}', '{apePaterno.ToString()}', '{apeMaterno.ToString()}', {especialidad.ToString()}, '{respuestas.ToString()}','{condicion}', {puntaje.ToString()}, {UserLoginCache.IdUser}, {tema.ToString()})";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                    
                }
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    //update Postulante set Condicion = 'INGRESO' where IdPostulante IN(SELECT TOP {vacantes[especialidadd-1]} IdPostulante FROM Postulante where Postulante.Especialidad = {especialidadd.ToString()} order by (select Puntaje.puntaje from Puntaje inner join Postulante on Puntaje.IdPuntaje=Postulante.Puntaje where Postulante.Especialidad={especialidadd.ToString()}) desc);
                    //command.CommandText = $"update Postulante set Condicion = 'INGRESO' where idPostulante IN(SELECT TOP {vacantes[especialidadd - 1]} IdPostulante FROM Postulante where Especialidad = {tema.ToString()} order by Condicion desc)";
                    
                    command.CommandText = $"update Postulante set Condicion = 'INGRESO' where IdPostulante IN(SELECT TOP {vacantes[especialidad - 1]} IdPostulante FROM Postulante where Postulante.Especialidad = {especialidad.ToString()} order by (select TOP 1 Puntaje.puntaje from Puntaje inner join Postulante on Puntaje.IdPuntaje=Postulante.Puntaje where Postulante.Especialidad={especialidad.ToString()}) desc)";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }


            }
        }

        public void Save(int save)
        {
            SaveGenerate.Save = save;
        }
        public void Fresh(int refresh)
        {
            Freshh.fresh = refresh;
        }
    }
}