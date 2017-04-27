using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using INPDS_App.CityService;
using INPDS_App.GeoService;
using INPDS_App.PriceServiceReference;
using INPDS_Core.Controller;
using INPDS_Core.DataAccess;
using INPDS_Core.DTO;
using INPDS_Core.Model;
using Credentials = INPDS_App.CityService.Credentials;
using MessageBox = System.Windows.MessageBox;

namespace INPDS_App.View
{
    /// <summary>
    /// Interaction logic for RegisterInvoice.xaml
    /// </summary>
    public partial class RegisterInvoice : Window
    {
        private readonly IUserController _userController;
        private readonly PriceServiceClient _priceServiceClient;

        public RegisterInvoice()
        {
            InitializeComponent();
            _priceServiceClient = new PriceServiceClient();
            _userController = UserController.Instance;
          
            Title += " | Uživatel: " + _userController.LoggedUser.UserName;
        }
 
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            dgOrders.SelectedIndex = -1;
            txBoxPrice.Text = "";
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double cena = double.Parse(txBoxPrice.Text);
                var invoice = new Invoice((Order) dgOrders.SelectedItem, cena);

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

        private async void dgOrders_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgOrders.SelectedIndex >= 0)
            {
                Order order = (Order) dgOrders.SelectedItem;
                double distance = 0;

                try
                {
                    distance = GetDistanceBetweenCities(order.From, order.To);
                    CalculateShowPriceGui(distance);
                }
                catch (Exception)
                {
                    DistanceErrorWindow.Show();
                    DistanceErrorWindow.Closed += (o, args) =>
                    {
                        if (DistanceErrorWindow.DialogResult != null && DistanceErrorWindow.DialogResult.Value)
                        {
                            distance = double.Parse(tbdistance.Text);
                            CalculateShowPriceGui(distance);
                        }
                    };
                }
            }
        }

        private async void CalculateShowPriceGui(double distance)
        {
            gridCalculate.Visibility = Visibility.Visible;
            var result = await _priceServiceClient.CalculatePriceAsync(distance, "ReturnFreight");
            txBoxPrice.Text = result.ToString();
            gridCalculate.Visibility = Visibility.Collapsed;
        }

        #region Bing SOAP Services

        private double GetDistanceBetweenCities(string mestoA, string mestoB)
        {
            gridCalculate.Visibility = Visibility.Visible;
            var request = new RouteRequest();
            request.Credentials = new Credentials();
            request.Credentials.ApplicationId = Properties.Resources.BingKey;
            List<Waypoint> waypoints = new List<Waypoint>(2);

            waypoints.Add(GetCityWaypoint(mestoA));
            waypoints.Add(GetCityWaypoint(mestoB));

            request.Waypoints = waypoints.ToArray();
            IRouteService client = new RouteServiceClient("BasicHttpBinding_IRouteService");
            RouteResponse routeResponse = client.CalculateRoute(request);
            gridCalculate.Visibility = Visibility.Collapsed;
            return routeResponse.Result.Summary.Distance;
        }

        private Waypoint GetCityWaypoint(string mestoA)
        {
            var result = GeocodeAddress(mestoA);
            Waypoint waypoint = new Waypoint();
            waypoint.Location = new CityService.Location();
            waypoint.Location.Latitude = result.Results[0].Locations[0].Latitude;
            waypoint.Location.Longitude = result.Results[0].Locations[0].Longitude;
            return waypoint;
        }

        private GeocodeResponse GeocodeAddress(string address)
        {
            GeocodeRequest geocodeRequest = new GeocodeRequest();
            geocodeRequest.Credentials = new GeoService.Credentials();
            geocodeRequest.Credentials.ApplicationId = Properties.Resources.BingKey;
            geocodeRequest.Query = address;

            GeocodeOptions geocodeOptions = new GeocodeOptions();
            geocodeRequest.Options = geocodeOptions;

            IGeocodeService geoService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            return geoService.Geocode(geocodeRequest);
        }

        #endregion

        private void btnDistanceOk_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbdistance.Text))
            {
                DistanceErrorWindow.DialogResult = true;
                DistanceErrorWindow.Close();
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DistanceErrorWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnDistanceOk_Click(sender, e);
            }
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnConfirm_Click(sender, e);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            _userController.Logout();
            var loginView = new LoginView();
            loginView.Show();
            Close();
        }
    }
}