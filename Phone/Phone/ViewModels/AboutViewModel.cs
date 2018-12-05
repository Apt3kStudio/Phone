using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Phone.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            //OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://cttonz.com")));
            SendTowatch = new Command(() => Device.OpenUri(new Uri("https://ctoonz.com")));
        }

        public ICommand OpenWebCommand { get; }
        public Command SendTowatch { get; private set; }
    }
}