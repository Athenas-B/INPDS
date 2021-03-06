﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using INPDS_Core.Controller;
using INPDS_Core.Model;

namespace INPDS_App.View
{
    /// <summary>
    ///     Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        private readonly User _user;
        private readonly IUserController _userController;


        public OrderView()
        {
            InitializeComponent();
            _userController = UserController.Instance;
            _user = _userController.LoggedUser;

            Title += " | Uživatel: " + _user.UserName;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _userController.Logout();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            tbTo.Clear();
            tbFrom.Clear();
            dtpItemsReady.Text = "";
            dtpItemsDeadline.Text = "";
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lbError.Foreground = Brushes.Crimson;
            try
            {
                var order = new Order(_user, (DateTime) dtpItemsDeadline.Value, tbFrom.Text,
                    (DateTime) dtpItemsReady.Value, tbTo.Text);

                IOrderController orderController = new OrderController();
                var result = orderController.RegisterOrder(order);

                lbError.Content = "";
                if (result.IsValid)
                {
                    lbError.Content = "Vložení proběhlo úspěšně";
                    lbError.Foreground = Brushes.GreenYellow;
                }
                else
                {
                    var outMessage = "";
                    foreach (var message in result.GetMessages)
                    {
                        outMessage += message + "\n";
                    }
                    MessageBox.Show(outMessage, "Nastala Chyba", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {
                lbError.Content = "Vyplňte všechna pole.";
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnConfirm_Click(sender, e);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Logout.From(this);
        }
    }
}