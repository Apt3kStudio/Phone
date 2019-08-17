using Phone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phone.Views.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerticalListDataTemplateSelectorPage : ContentPage
    {
        public VerticalListDataTemplateSelectorPage()
        {
            InitializeComponent();
            BindingContext = new MonkeysViewModel();
        }
    }
}