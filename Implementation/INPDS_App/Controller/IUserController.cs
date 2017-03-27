using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using INPDS_App.Model;

namespace INPDS_App.Controller
{
    public interface IUserController
    {
        bool IsLoggedIn();
        User LoggedUser();
        void Loggin(string userName, string userPassword);
        void LogOut();
    }
}


