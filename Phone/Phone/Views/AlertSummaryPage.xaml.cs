using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Phone.Models;
using Phone.ViewModels;
using Plugin.LocalNotifications;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertSummaryPage : ContentPage
    {
        AlertDetailViewModel viewModel;

        public AlertSummaryPage(AlertDetailViewModel viewModel)
        {
            InitializeComponent();

            CrossLocalNotifications.Current.Show("Range Alert", viewModel.alert.Message);


            BindingContext = this.viewModel = viewModel;
        }

        public AlertSummaryPage()
        {
            InitializeComponent();

            var alert = new AlertModel
            {
                Id = UtilityHelper.getNewID().ToString(),
                Name = "",
                Message = ""
            };

            viewModel = new AlertDetailViewModel(alert);
            BindingContext = viewModel;
        }
    }
}