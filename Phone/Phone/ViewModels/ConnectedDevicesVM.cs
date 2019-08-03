using Android.Bluetooth;
using Phone.Models;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phone.ViewModels
{
    public class ConnectedDevicesVM: BaseVM
    {
        public ConnectedDevicesVM()
        {
            RegisteredDevices = new ObservableCollection<RegisteredDevice>();
            LoadItemsCommand = new Command(() =>ExecuteLoadItemsCommand());
        }
        public ObservableCollection<RegisteredDevice> RegisteredDevices { get; set; }
        public Command LoadItemsCommand { get; set; }
        void ExecuteLoadItemsCommand()
        {
            RegisteredDevices.Add(new RegisteredDevice
            {
                Id = "1", Description = "Jonh Doe's Watch is connected",
                Text = "Moto Watch", deviceType = "Wear",
                device = "LRS 365", deviceName = "LRS",
                manufacturer ="Motorola", version = "7.0",
                platform ="Android", idiom="Watch",
                 Flash = "Flash",
                 Sound = "Sound",
                 Vibration = "Vibration",
                  Distance = "25"
            });
            RegisteredDevices.Add(new RegisteredDevice
            {
                Id = "1",
                Description = "Micheal Smith's watch is Connected",
                Text = "Samsung Watch",
                deviceType = "Wear",
                device = "Galaxy Watch",
                deviceName = "LRS",
                manufacturer = "Samsung",
                version = "10.0",
                platform = "Android",
                idiom = "Watch",
                Flash = "Flash",
                Sound = "Sound",
                Vibration = "Vibration",
                Distance = "43"
            });
            RegisteredDevices.Add(new RegisteredDevice
            {
                Id = "1",
                Description = "Smatt Sung's Phone is connected",
                Text = "OnePlus",
                deviceType = "Phone",
                device = "OnePlus 5.0",
                deviceName = "OnePlus 5",
                manufacturer = "OnePlus",
                version = "5.0",
                platform = "Android",
                idiom = "Phone",
                Flash = "Flash",
                Sound = "Sound",
                Vibration = "Vibration",
                Distance = "34"
            });


        }
       

        
       
    }
}
