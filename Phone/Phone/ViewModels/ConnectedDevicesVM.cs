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
       
        
        private DeviceService dServ;
        private MockService mockDb;
        private bool isMock { get; set; }

        public RegisteredDevice SelectedUnRegDevic
        {
            get
            {
                return SelectedUnRegDevic;
            }
            set
            {
                if (SelectedUnRegDevic != value)
                {
                    SelectedUnRegDevic = value;
                }
            }
        }

        string _Distance = "";
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
        public ConnectedDevicesVM()
        {
            
            RegisteredDevices = new ObservableCollection<RegisteredDevice>();
            UnRegisteredDevices = new ObservableCollection<RegisteredDevice>();
           
            dServ = new DeviceService();     
            dServ.SubscribeToUnregisteredDevicesDiscovered += OnDeviceDiscovery;
            mockDb = new MockService();
        }

        public void RegisterDevice(RegisteredDevice regDevc)
        {
            Task.Run(async () => {
                await UtilityHelper.SaveToPhoneAsync(regDevc.deviceName,"Registered");
                RefreshDevicesColls();
            });
        }

        public void UnRegisterDevice()
        {
            Task.Run(async () => {
                await UtilityHelper.SaveToPhoneAsync("Ticwatch E P59N", "");
                RefreshDevicesColls();
            });            
        }

        internal void loadUnregisteredDevices()
        {           
            dServ.InitiateDiscovery();         
        }
        /// <summary>
        /// This Method will Load Devices based on sample data from the MockService. It also execute the main logic. 
        /// Todo We need to find the best location for the MainLogic. I think we should branch out to a new class where we can continue working refining the main logic.
        /// </summary>
        internal void loadRegisteredDevices(bool ExecuteMainLogic = false, int NumberOfMockDevices=0)
        {
             Task.Run(async()=>
             {
                await mockDb.MockLoadRegistedDevices(RegisteredDevices, Distance, NumberOfMockDevices);
                 if (ExecuteMainLogic)
                 {
                     MainLogic();
                 }
                 
            });
        }
        /// <summary>
        /// This Method  is subscribe to a device discovery methong on Device Service. The method is called SubscribeToUnregisteredDevicesDiscovered
        /// </summary>
        /// <param name="nodes"></param>
        /// 
        public void RefreshDevicesColls()
        {
            UnRegisteredDevices.Clear();
            RegisteredDevices.Clear();
            loadUnregisteredDevices();
            loadRegisteredDevices(false,3);
        }
        private void OnDeviceDiscovery(List<INode> nodes)
        {                
            InsertToDeviceCollections(nodes);
        }   
        /// <summary>
        /// Connected Devices will be inserted to a device collection. There are two collections: Registered and UnRegistered.
        /// </summary>
        /// <param name="nodes"></param>
        private void InsertToDeviceCollections(List<INode> nodes)
        {
            foreach (INode node in nodes)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(AddToRegOrUnRegDevicesColletion(node));
            }
        }
        /// <summary>
        /// This code runs on the main thread
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Action AddToRegOrUnRegDevicesColletion(INode node)
        {
            return () =>
            {
                if (!UtilityHelper.doesItExit(node.DisplayName))
                {
                    UnRegisteredDevices.Add(CreateConnDevice(node));
                }
                else
                {
                    RegisteredDevices.Add(CreateConnDevice(node));
                }
            };
        }

        private static RegisteredDevice CreateConnDevice(INode node)
        {
            return new RegisteredDevice
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
                ImageSource = "https://apt3k.azurewebsites.net/images/ticwatch.png"
            };
        }

        void MainLogic()
        {
            Models.Device ThisPhone = new Models.Device();
            foreach (RegisteredDevice watch in RegisteredDevices)
            {
                ThisPhone.MainLogic(watch);
            }
        }
        public async Task SaveCurrentCountAsync()
        {
            await UtilityHelper.SaveToPhoneAsync("stampcounter", Distance);
        }       
       
    }
}
