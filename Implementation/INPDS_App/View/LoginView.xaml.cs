using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using INPDS_Core.Controller;
using INPDS_Core.Model;

namespace INPDS_App.View
{
    /// <summary>
    ///     Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly Dictionary<UserRole, Func<Window>> _userWindows = new Dictionary<UserRole, Func<Window>>();

        public LoginView()
        {
            InitializeComponent();
            _userWindows[UserRole.Customer] = () => new OrderView();
            _userWindows[UserRole.Accountant] = () => new RegisterInvoice();
            _userWindows[UserRole.Planner] = () => new PlannerView();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            IUserController userController = UserController.Instance;
            try
            {
                userController.Login(tbLogin.Text, tbPassword.Password);

                if (userController.IsLoggedIn)
                {
                    var window = _userWindows[userController.LoggedUser.UserRole]();
                    window.Show();
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

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}