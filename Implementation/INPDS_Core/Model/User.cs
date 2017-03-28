namespace INPDS_Core.Model
{
    public class User
    {
        public User(int idUser, string userName, string password, UserRole userRole)
        {
            IdUser = idUser;
            UserName = userName;
            Password = password;
            UserRole = userRole;
        }

        public int IdUser { get; set; }
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