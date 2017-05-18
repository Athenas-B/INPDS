using System.Windows;
using INPDS_Core.Controller;

namespace INPDS_App.View
{
    public static class Logout
    {
        public static void From(Window parent)
        {
            UserController.Instance.Logout();
            var loginView = new LoginView();
            loginView.Show();
            parent.Close();
        }
    }
}