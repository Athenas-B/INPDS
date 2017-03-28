using System;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public class UserController : IUserController
    {
        public User LoggedUser { get; private set; }

        public bool IsLoggedIn
        {
            get { return LoggedUser != null; }
        }

        public void Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}