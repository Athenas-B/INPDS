using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPDS_App.Model
{
    public class User
    {
        
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }


        public User(int idUser, string userName, string password, Role userRole)
        {
            this.IdUser = idUser;
            this.UserName = userName;
            this.Password = password;
            this.UserRole = userRole;
        }

    }

    public enum Role
    {
        CUSTOMER,
        ACCOUNTANT,
        PLAYNNER
    }
}