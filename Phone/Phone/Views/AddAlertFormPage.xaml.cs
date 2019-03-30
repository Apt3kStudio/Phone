using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Phone.Models;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertFormPage : ContentPage
    {
        public AlertModel alert { get; set; }

        public AlertFormPage()
        {
            InitializeComponent();

            alert = new AlertModel
            {
                Id = UtilityHelper.getNewID().ToString(),
                Name = "V",
                Message = "V"
            };

            BindingContext = alert;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddAlert", alert);
            await Navigation.PopModalAsync();
        }
    }
}