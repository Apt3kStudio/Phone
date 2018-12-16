using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Phone.Models;
using Phone.ViewModels;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertSummaryPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public AlertSummaryPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

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

            viewModel = new ItemDetailViewModel(alert);
            BindingContext = viewModel;
        }
    }
}