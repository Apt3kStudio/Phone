using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Phone.Models;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPage : ContentPage
    {
        public AlertModel Item { get; set; }

        public AlertPage()
        {
            InitializeComponent();

            Item = new AlertModel
            {
                Name = "Alert name",
                Message = "This is an alert description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddAlert", Item);
            await Navigation.PopModalAsync();
        }
    }
}