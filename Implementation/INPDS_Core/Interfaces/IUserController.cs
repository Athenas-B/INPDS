using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public interface IUserController
    {
        User LoggedUser { get; }
        bool IsLoggedIn { get; }

        /// <param name="username"></param>
        /// <param name="password"></param>
        void Login(string username, string password);

        void Logout();
    }
}