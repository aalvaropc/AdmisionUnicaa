using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Common.Cache;
namespace Domain
{
    public class UserModel
    {
        UserDao userDao = new UserDao();
        //EDITADO------------------------------------
        //atributos
        private int idUser;
        private string loginName;
        private string password;
        private string firstName;
        private string lastName;
        private string email;

        public UserModel(int idUser, string loginName, string password, string firstName, string lastName, string email)
        {
            this.IdUser = idUser;
            this.LoginName = loginName;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }
        public UserModel()
        {

        }
        public string editUserProfile()
        {
            try
            {
                userDao.editProfile(idUser, loginName, password, firstName, lastName, email);
                LoginUser(loginName, password);
                return "Your profile as been successfully update";
            }
            catch (Exception ex)
            {
                return "Username is already registered, try another";
            }
        }


        public int IdUser { get => idUser; set => idUser = value; }
        public string LoginName { get => loginName; set => loginName = value; }
        public string Password { get => password; set => password = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }


        //EDITADO------------------------------------
        public bool LoginUser(string user, string pass)
        {
            return userDao.Login(user, pass);
        }
    }
}