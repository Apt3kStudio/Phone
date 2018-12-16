﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Phone.Models;
using Phone.Views;
using Phone.ViewModels;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertListPage : ContentPage
    {
        SettingViewModel viewModel;

        public AlertListPage()
        {
            InitializeComponent();
            viewModel = new SettingViewModel();
            BindingContext = viewModel;
    }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as AlertModel;
            if (item == null)
                return;

            await Navigation.PushAsync(new AlertSummaryPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AlertFormPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.alerts.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}