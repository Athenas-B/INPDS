using System.Linq;
using System.Security.Cryptography;
using System.Text;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public class UserController : IUserController
    {
        private static UserController _instance;

        private UserController()
        {
        }

        public User LoggedUser { get; private set; }

        public bool IsLoggedIn
        {
            get { return LoggedUser != null; }
        }

        public static UserController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserController();
                }
                return _instance;
            }
        }

        public void Login(string username, string password)
        {
            using (var context = new ReturnFreightContext())
            {
                using (SHA512 sha = new SHA512Managed())
                {
                    var encoding = Encoding.UTF8;
                    var hash = encoding.GetString(sha.ComputeHash(encoding.GetBytes(password)));
                    var foundUser =
                        context.Users.FirstOrDefault(user => user.UserName == username && user.Password == hash);
                    if (foundUser != null)
                    {
                        LoggedUser = foundUser;
                    }
                }
            }
        }

        public void Logout()
        {
            LoggedUser = null;
        }
    }
}