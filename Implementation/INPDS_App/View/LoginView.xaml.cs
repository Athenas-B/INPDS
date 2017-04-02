using System;
using System.Windows;
using INPDS_Core.Controller;

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
            }
            catch (Exception exception)
            {
                lbError.Content = "Unknown error";
            }
        }
    }
}
