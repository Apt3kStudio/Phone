using Android.Bluetooth;
using Android.Gms.Wearable;
using Phone.Models;
using Phone.Services;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phone.ViewModels
{

    public class ConnectedDevicesVM : BaseVM
    {
        public ObservableCollection<RegisteredDevice> RegisteredDevices { set; get; }
        public ObservableCollection<RegisteredDevice> UnRegisteredDevices { set; get; }
        public ICommand unregisteredCMD { get; }

        private bool isUnRegistered = false;
        private DeviceService dServ;
        private MockService mockDb;

        string _Distance = "";
        public ConnectedDevicesVM()
        {
            unregisteredCMD = new Command(UnRegisterDevice);
            RegisteredDevices = new ObservableCollection<RegisteredDevice>();
            UnRegisteredDevices = new ObservableCollection<RegisteredDevice>();
            dServ = new DeviceService();     
            dServ.SubscribeToUnregisteredDevicesDiscovered += OnDeviceDiscovery;
            mockDb = new MockService();
        }

        void UnRegisterDevice()
        {
            Task.Run(async () => {
                await UtilityHelper.SaveToPhoneAsync("Ticwatch E P59N", "");
            });            
        }

        internal void loadUnregisteredDevices()
        {
            isUnRegistered = true;
            dServ.InitiateDiscovery();         
        }
        internal void loadRegisteredDevices()
        {
            
                RegisteredDevices = mockDb.MockLoadRegistedDevices();
            
        }
        private void OnDeviceDiscovery(List<INode> nodes)
        {
            switch (isUnRegistered)
            {                
                case true:
                        populateConnectedDevices(nodes, UnRegisteredDevices);
                    isUnRegistered = false;
                break;
            }
        }   
        private void populateConnectedDevices(List<INode> nodes, ObservableCollection<RegisteredDevice> Devices)
        {
            foreach (INode node in nodes)
            {   Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (!UtilityHelper.doesItExit(node.DisplayName))
                    {                                  
                        Devices.Add(new RegisteredDevice
                        {
                            Id = "1",
                            Description = "Smatt Sung's Phone is connected",
                            Text = "OnePlus",
                            deviceType = "Phone",
                            device = node.DisplayName.ToUpper(),
                            deviceName = node.DisplayName,
                            manufacturer = "OnePlus",
                            version = "5.0",
                            platform = "Android",
                            idiom = "Phone",
                            Flash = "Flash",
                            Sound = "Sound",
                            Buzz = false,
                            Distance = "34",
                            Measurement = "ft",
                            ImageSource = "https://images-na.ssl-images-amazon.com/images/I/51RGtl9zoCL._SL1000_.jpg"
                        });
                    }
                    else
                    {
                        //await UtilityHelper.SaveToPhoneAsync(node.DisplayName, "Registered");
                        RegisteredDevices.Add(new RegisteredDevice
                        {
                            Id = "1",
                            Description = "Smatt Sung's Phone is connected",
                            Text = "OnePlus",
                            deviceType = "Phone",
                            device = node.DisplayName.ToUpper(),
                            deviceName = node.DisplayName,
                            manufacturer = "OnePlus",
                            version = "5.0",
                            platform = "Android",
                            idiom = "Phone",
                            Flash = "Flash",
                            Sound = "Sound",
                            Buzz = false,
                            Distance = "34",
                            Measurement = "ft",
                            ImageSource = "https://images-na.ssl-images-amazon.com/images/I/51RGtl9zoCL._SL1000_.jpg"
                        });                        
                    }
                });
            }
        }
        public Command LoadItemsCommand { get; set; }
        void LoadUnRegisteredDevices()
        {
           
                RegisteredDevices.Add(new RegisteredDevice
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
                Buzz = false,
                Measurement = "ft",
                ImageSource = "https://png2.kisspng.com/sh/05c12b0d93e8fac1dfc4b5b620529203/L0KzQYm3VMA4N5lAfZH0aYP2gLBuTf1wfJCyS6g5LULxdH7uhf5mepJ5gdH3LXHwccv2jr1kd54yi99qcoT6ccXqiL1xbV58eeZsaHX2PYbog8kxbWM3S9UCYXG1PoWAV8U3OWQ9Sac7M0G1RYiCVMI1P2gziNDw/kisspng-moto-360-2nd-generation-amazon-com-smartwatch-pe-watches-5ac90e223c7aa2.4775613815231257942477.png"
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
