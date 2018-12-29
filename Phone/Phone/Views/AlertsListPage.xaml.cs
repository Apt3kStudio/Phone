using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Phone.Models;
using Phone.Views;
using Phone.ViewModels;
using Plugin.LocalNotifications;
using Xamarin.Essentials;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertListPage : ContentPage
    {
        SettingViewModel viewModel;

        public AlertListPage()
        {
            CrossLocalNotifications.Current.Show("PhoneApp", "Running..");
            InitializeComponent();
            viewModel = new SettingViewModel();
            BindingContext = viewModel;
    }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var alert = args.SelectedItem as AlertModel;
            if (alert == null)
                return;

            await Navigation.PushAsync(new AlertSummaryPage(new AlertDetailViewModel(alert)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("This is an alert", "This is the alert Message", "OK");
            await Navigation.PushModalAsync(new NavigationPage(new AlertFormPage()));
        }
        async void goToSettings(object sender, EventArgs e)
        {
          //  await DisplayAlert("This is an alert", "This is the alert Message", "OK");
            await Navigation.PushModalAsync(new NavigationPage(new settingpage()));
        }

       protected void PushVibrate_Clicked(object sender, EventArgs e)
        {
            try
            {
                Vibration.Vibrate();
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        public void CancelVibration()
        {
            try
            {
                Vibration.Cancel();
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.alerts.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}