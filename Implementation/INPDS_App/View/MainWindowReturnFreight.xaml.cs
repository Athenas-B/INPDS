using System.Windows;
using INPDS_Core.Controller;

namespace INPDS_App.View
{
    /// <summary>
    ///     Interaction logic for MainWindowReturnFreight.xaml
    /// </summary>
    public partial class MainWindowReturnFreight : Window
    {
        private readonly IUserController _userController;

        public MainWindowReturnFreight()
        {
            InitializeComponent();
        }

        public MainWindowReturnFreight(IUserController usrControl)
        {
            InitializeComponent();
            _userController = usrControl;
        }

        private void btnRegisterOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderView = new OrderView(_userController);
            orderView.Show();
            Close();
        }

        private void btnRegisterInvoice_Click(object sender, RoutedEventArgs e)
        {
            var registerInvoiceView = new RegisterInvoice(_userController);
            registerInvoiceView.Show();
            Close();
        }
    }
}
