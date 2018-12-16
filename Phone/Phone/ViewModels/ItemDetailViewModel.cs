using System;

using Phone.Models;

namespace Phone.ViewModels
{
    public class AlertDetailViewModel : BaseViewModel
    {
        public AlertModel alert { get; set; }
        public AlertDetailViewModel(AlertModel alert = null)
        {
            Title = alert?.Name;
            this.alert = alert;
        }
    }
}
