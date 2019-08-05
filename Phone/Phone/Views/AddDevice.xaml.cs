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
    public partial class AddDevice : ContentPage
    {
        public string icon { get; set; }
        public AddDevice()
        {
            icon = FontAwesomeIcons.BatteryQuarter;
            InitializeComponent();
            LayoutChanged += LoginPage_LayoutChanged;

        }

        private void LoginPage_LayoutChanged(object sender, EventArgs e)
        {
            if (SelectorBackground.Height < 0) return;
            SelectorBackground.CornerRadius = SelectorBackground.Height / 2;

        }
    }
}