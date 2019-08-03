using Phone.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public string icon { get; set; }
        public Settings()
        {
            icon = FontAwesomeIcons.BatteryQuarter;
            InitializeComponent();
        }
    }
}