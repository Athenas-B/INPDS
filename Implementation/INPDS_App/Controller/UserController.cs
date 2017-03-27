
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INPDS_App.Model;

namespace INPDS_App.Controller
{
    public class UserController : IUserController
    {
        private User loggedUser;


        public User LoggedUser()
        {
            return loggedUser;
        }


        public bool IsLoggedIn()
        {
            return loggedUser != null;
        }

        public void Loggin(string userName, string userPassword)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            loggedUser = null;
        }
    }
}