using Android.Bluetooth;
using Phone.Models;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phone.ViewModels
{

    public class ConnectedDevicesVM : BaseVM
    {
        string _Distance = "";
        public ConnectedDevicesVM()
        {
            RegisteredDevices = new ObservableCollection<RegisteredDevice>();
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());
        }
        public ObservableCollection<RegisteredDevice> RegisteredDevices { get; set; }
        public Command LoadItemsCommand { get; set; }
        void ExecuteLoadItemsCommand()
        {
            Models.Device this_device = new Models.Device();
            RegisteredDevices.Add(new RegisteredDevice
            {
                Id = "1",
                Description = "Smatt Sung's Phone is connected",
                Text = "OnePlus",
                deviceType = "Phone",
                device = "OnePlus 5.0".ToUpper(),
                deviceName = "OnePlus 5",
                manufacturer = "OnePlus",
                version = "5.0",
                platform = "Android",
                idiom = "Phone",
                Flash = "Flash",
                Sound = "Sound",
                Vibration = "Vibration",
                Distance = _Distance,
                Measurement = "ft"

            });

            foreach (RegisteredDevice watch in RegisteredDevices)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    if (this_device.HandShake(watch))
                    {
                        int i = 0;
                        await UtilityHelper.SaveToPhoneAsync("PreviousTimeStamp", "");
                        await Task.Run(async () =>
                        {
                            while (i < 10)
                            {
                                this_device.CurrentElapsedTime = this_device.Trip(watch);
                                if (string.IsNullOrEmpty(UtilityHelper.RetrieveFromPhone("PreviousTimeStamp").ToString()))
                                {
                                    await UtilityHelper.SaveToPhoneAsync("PreviewsTimeStamp", this_device.CurrentElapsedTime.ToString());
                                }
                                this_device.CalculateProximityStatus();
                                watch.Distance = this_device.CurrentElapsedTime.ToString();
                                i++;
                            }
                        });
                    }
                });
            }
        }
        public async Task SaveCurrentCountAsync()
        {
            await UtilityHelper.SaveToPhoneAsync("stampcounter", Distance);
        }
        public async Task<bool> GetCount()
        {
            return await Task.Run(async () =>
            {
                for (var i = 0; i < 10000; i++)
                {
                    await Task.Delay(1000);
                    Distance = i.ToString();
                }
                return true;
            });
        }
        public string Distance
        {
            get => _Distance;
            set
            {
                if (_Distance == value)
                    return;
                _Distance = value;
                NotifyPropertyChange(nameof(Distance));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChange(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
