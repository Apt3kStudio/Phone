using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

using Phone.Models;
using Phone.Views;

namespace Phone.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public ObservableCollection<AlertModel> alerts { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SettingViewModel()
        {
            Title = "Browse";
            alerts = new ObservableCollection<AlertModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<AlertFormPage, AlertModel>(this, "AddAlert", async (obj, alert) =>
            {
                var newAlert = alert as AlertModel;
                alerts.Add(newAlert);
                await DataStore.AddItemAsync(newAlert);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                alerts.Clear();
                var alertsFromDB = await DataStore.GetItemsAsync(true);
                foreach (var alertItem in alertsFromDB)
                {
                    alerts.Add(alertItem);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}