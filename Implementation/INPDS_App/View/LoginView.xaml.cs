using System;
using System.Windows;
using INPDS_Core.Controller;
using INPDS_Core.Model;

namespace INPDS_App.View
{
    /// <summary>
    ///     Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            IUserController userController = new UserController();
            try
            {
                userController.Login(tbLogin.Text, tbPassword.Password);

                if (userController.IsLoggedIn)
                {
                    var orderView = new OrderView(userController);
                    orderView.Show();
                    Close();
                }
                else
                {
                    lbError.Content = "Chybné uživatelské jméno nebo heslo.";
                }
            }
            catch (Exception)
            {
                lbError.Content = "Přihlášení se nezdařilo";
            }
        }
    }
}
