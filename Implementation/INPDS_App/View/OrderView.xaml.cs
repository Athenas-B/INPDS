using System;
using System.Windows;
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
        }

        public OrderView(IUserController usrControl)
        {
            InitializeComponent();
            _user = usrControl.LoggedUser;
            _userController = usrControl;
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
            try
            {
                var order = new Order(_user, (DateTime) dtpItemsDeadline.Value, tbFrom.Text,
                    (DateTime) dtpItemsReady.Value, tbTo.Text);

                IOrderController orderController = new OrderController();
                orderController.RegisterOrder(order);
            }
            catch (Exception exception)
            {
                lbError.Content = "Unknown Error";
            }
        }
    }
}
