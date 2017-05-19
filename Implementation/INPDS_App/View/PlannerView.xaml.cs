using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using INPDS_Core.Controller;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;

namespace INPDS_App.View
{
    /// <summary>
    ///     Interaction logic for PlannerView.xaml
    /// </summary>
    public partial class PlannerView : Window
    {
        private readonly ITripPlanner _planner;

        public PlannerView()
        {
            InitializeComponent();
            _planner = TripPlanner.CreateTripPlanner();
            var user = UserController.Instance.LoggedUser;
            Title += " | Uživatel: " + user.UserName;
        }

        private bool IsTripSelected
        {
            get { return comboTrips.SelectedItem is Trip; }
        }

        private bool IsOrderSelected
        {
            get { return dgOrders.SelectedItem is Order; }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Logout.From(this);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new ReturnFreightContext())
            {
                context.Users.Load();
                context.Orders.Load();
                context.Trips.Load();
                var orders =
                    context.Orders.SqlQuery(
                        "select o.* from Orders o where o.Id not in (select ISNULL(PrimaryOrder_Id, 0) from Trips union all select ISNULL(SecondaryOrder_Id, 0) from Trips)")
                        .ToList();
                dgOrders.ItemsSource = orders;
                comboTrips.ItemsSource = context.Trips.Where(trip => trip.SecondaryOrder == null).ToList();
                dgTrips.ItemsSource = context.Trips.Local;
            }
        }

        private void btnNewTrip_Click(object sender, RoutedEventArgs e)
        {
            var order = dgOrders.SelectedItem as Order;
            if (order == null) return;
            _planner.PlanTrip(order);
            LoadData();
        }

        private void btnAddToExistingTrip_Click(object sender, RoutedEventArgs e)
        {
            var order = dgOrders.SelectedItem as Order;
            var trip = comboTrips.SelectedItem as Trip;
            if (order == null || trip == null) return;
            _planner.PlanTrip(trip, order);
            LoadData();
        }

        private void dgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddToExistingTrip.IsEnabled = IsOrderSelected && IsTripSelected;
            btnNewTrip.IsEnabled = IsOrderSelected;
        }

        private void comboTrips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddToExistingTrip.IsEnabled = IsOrderSelected && IsTripSelected;
        }

        private void dgTrips_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var displayName = GetPropertyDisplayName(e.PropertyDescriptor);

            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }


        public static string GetPropertyDisplayName(object descriptor)
        {
            var pd = descriptor as PropertyDescriptor;

            if (pd != null)
            {
                var displayName = pd.Attributes[typeof (DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && !Equals(displayName, DisplayNameAttribute.Default))
                {
                    return displayName.DisplayName;
                }
            }
            else
            {
                var pi = descriptor as PropertyInfo;
                if (pi == null) return null;
                var attributes = pi.GetCustomAttributes(typeof (DisplayNameAttribute), true);
                return (from t in attributes select t as DisplayNameAttribute into displayName where displayName != null && !Equals(displayName, DisplayNameAttribute.Default) select displayName.DisplayName).FirstOrDefault();
            }

            return null;
        }
    }
}