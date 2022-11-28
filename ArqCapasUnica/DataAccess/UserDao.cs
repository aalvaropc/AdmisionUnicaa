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
        //EDITADO---------------------------------
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
        //EDITADO---------------------------------
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
        int Idespecialidad = 0;
        string[] vacantes = { "12", "15", "20", "20", "18", "25", "22", "24", "30", "15", "18", "25", "22", "24", "30", "22", "24", "30", "15", "18", "22", "24", "30", "22", "24" , "18", "22", "24", "30", "22" , "30", "22", "24", "30", "15", "18", "22", "24", "30", "22"};
        public void generate(string idPostulante, string nombre, string apePaterno, string apeMaterno, int especialidad, string respuestas, string condicion, int puntaje, int tema)
        {
            using (var connection = GetConnection())
            {
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
                    command.CommandText = $"update Postulante set Condicion = 'INGRESO' where idPostulante IN(SELECT TOP {vacantes[tema-1]} IdPostulante FROM Postulante where Especialidad = {tema.ToString()} order by Condicion desc)";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }


        }

    }
}