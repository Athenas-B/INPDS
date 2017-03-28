using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INPDS_Core.Model
{
    public class User
    {
        public User()
        {
            //Empty constructor
        }

        public User(string userName, string password, UserRole userRole)
        {
            UserName = userName;
            Password = password;
            UserRole = userRole;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }

    public enum UserRole
    {
        Customer,
        Accountant,
        Planner
    }
}