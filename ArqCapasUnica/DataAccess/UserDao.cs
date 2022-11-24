using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common.Cache;

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

        public SqlDataAdapter llenarTabla(int opc, String buscar)
        {
            SqlDataAdapter adapter;
            switch (opc) 
            {
                case 0:
                    adapter = new SqlDataAdapter("SELECT CODPOST AS 'Código', APPAT AS 'Ap. Paterno', APMAT AS 'Ap. Materno', NOMBS AS 'Nombres' FROM POSTULANTES", GetConnection());
                    break;
                case 1: 
                    adapter = new SqlDataAdapter(
                    "SELECT CODPOST AS 'Código', APPAT AS 'Ap. Paterno', APMAT AS 'Ap. Materno', NOMBS AS 'Nombres' FROM POSTULANTES WHERE CODPOST = '" + buscar + "' OR APPAT = '" + buscar + "' OR APMAT = '" + buscar + "'"
                    , GetConnection());
                    break;
                case 2:
                    adapter = new SqlDataAdapter(
                    "SELECT p.CODPOST AS 'Código', p.APPAT AS 'Ap. Paterno', p.APMAT AS 'Ap. Materno', p.NOMBS AS 'Nombres' FROM POSTULANTES p INNER JOIN AREA a ON p.CODPOST = a.CODPOST_FK INNER JOIN FACULTAD f ON p.CODPOST = f.CODPOST_FK WHERE a.CODAREA = '" + buscar + "' OR f.CODFACU = '" + buscar + "'"
                    , GetConnection());
                    break;
                default:
                    adapter = new SqlDataAdapter("SELECT * FROM POSTULANTES",GetConnection());
                    break;
            }
            return adapter;
        }
    }
}