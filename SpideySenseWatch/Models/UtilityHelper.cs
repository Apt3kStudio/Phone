using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SpideySenseWatch.Models
{
    public static class UtilityHelper
    {
        public static Context GetContext()
        {
            return Android.App.Application.Context;
        }
    }
}