using System;
using System.Data.Entity;
using System.Windows;
using INPDS_Core.Controller;
using INPDS_Core.DataAccess;
using INPDS_Core.DTO;
using INPDS_Core.Model;

namespace INPDS_App.View
{
    /// <summary>
    /// Interaction logic for RegisterInvoice.xaml
    /// </summary>
    public partial class RegisterInvoice : Window
    {
        private readonly IUserController _userController;
        public RegisterInvoice()
        {
            InitializeComponent();
        }
        public RegisterInvoice(IUserController usrController)
        {
            InitializeComponent();
            _userController = usrController;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindowReturnFreight(_userController);
            mainWindow.Show();
            Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TODO cena = webservice.calculateprice
                double cena = 0;
                var invoice = new Invoice((Order)dgOrders.SelectedItem, cena);

                IInvoiceController invoiceController = new InvoiceController();
                ValidationResult result = invoiceController.RegisterInvoice(invoice);

                if (result.IsValid)
                {
                    MessageBox.Show("Vložení bylo úspěšné");
                }
                else
                {
                    string outMessage = "";
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
                MessageBox.Show("Vyberte objednávku!", "Nastala Chyba", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            using (var context = new ReturnFreightContext())
            {

                context.Users.Load();
                context.Orders.Load();
                dgOrders.ItemsSource = context.Orders.Local;
            }
        }
    }
}
