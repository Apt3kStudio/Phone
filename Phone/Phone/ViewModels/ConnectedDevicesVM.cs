using Android.Bluetooth;
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
using Xamarin.Forms;

namespace Phone.ViewModels
{
    public class ConnectedDevicesVM: BaseVM
    {

        public ObservableCollection<RegisteredDevice> RegisteredDevices { set; get; }
        public ConnectedDevicesVM()
        {
            RegisteredDevices = new ObservableCollection<RegisteredDevice>();
            //RegisteredDevices.CollectionChanged += mycollChanged;
            LoadItemsCommand = new Command(() =>ExecuteLoadItemsCommand());
            DeviceService devicServ = new DeviceService();
            
        }

        //private void mycollChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.NewItems != null)
        //        foreach (RegisteredDevice item in e.NewItems)
        //            item.PropertyChanged += MyType_PropertyChanged;

        //    if (e.OldItems != null)
        //        foreach (RegisteredDevice item in e.OldItems)
        //            item.PropertyChanged -= MyType_PropertyChanged;
        //}

        //private void MyType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "Buzz")
        //    {

        //    }
              
        //}
        
        public Command LoadItemsCommand { get; set; }
        void ExecuteLoadItemsCommand()
        {
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
                Distance = "34",
                Measurement = "ft",
                ImageSource = "https://png2.kisspng.com/sh/05c12b0d93e8fac1dfc4b5b620529203/L0KzQYm3VMA4N5lAfZH0aYP2gLBuTf1wfJCyS6g5LULxdH7uhf5mepJ5gdH3LXHwccv2jr1kd54yi99qcoT6ccXqiL1xbV58eeZsaHX2PYbog8kxbWM3S9UCYXG1PoWAV8U3OWQ9Sac7M0G1RYiCVMI1P2gziNDw/kisspng-moto-360-2nd-generation-amazon-com-smartwatch-pe-watches-5ac90e223c7aa2.4775613815231257942477.png"
            });
            RegisteredDevices.Add(new RegisteredDevice
            {
                Id = "1",
                Description = "Micheal Smith's watch is Connected",
                Text = "Samsung Watch",
                deviceType = "Wear",
                device = "Galaxy Watch".ToUpper(),
                deviceName = "LRS",
                manufacturer = "Samsung",
                version = "10.0",
                platform = "Android",
                idiom = "Watch",
                Flash = "Flash",
                Sound = "Sound",
                Buzz = false,
                Distance = "43",
                Measurement = "ft",
                ImageSource = "https://png2.kisspng.com/sh/df27b0b495d17a96bd3d8b9c8136c834/L0KzQYm3VcE5N5ZnfZH0aYP2gLBuTf1wfJCyS6g5LULxdH7uhf5mepJ5gdH3LYPwccP7lBF1a5kyiAt3b378fcS0gf5lNWZmftcCYUW0c7SCVsk0Nmo8TKMENEK2QYa5VsYzPmkAT6o8OD7zfri=/kisspng-moto-360-2nd-generation-smartwatch-synonyms-and-5afe7a51cc9693.974194231526626897838.png"
            });

            RegisteredDevices.Add(new RegisteredDevice
            {
                Id = "1", Description = "Jonh Doe's Watch is connected",
                Text = "Moto Watch", deviceType = "Wear",
                device = "LRS 365".ToUpper(), deviceName = "LRS",
                manufacturer ="Motorola", version = "7.0",
                platform ="Android", idiom="Watch",
                 Flash = "Flash",
                 Sound = "Sound",
                 Buzz = false,
                  Distance = "25",
                  Measurement = "ft",
                ImageSource = "https://png2.kisspng.com/sh/24f713c4870c11d0ff47fd73edb19ce9/L0KzQYm3U8E4N6l7fZH0aYP2gLBuTfxoNZgyj9N9Y3iwgn70jCRwNWQ7SJ87bnSwd7b1hgJifJp0hp98YX32hbBuTfdmaV5xjepAcomwh7L7gBhme146edNtN0DpR4aChck3P183T6oCOUCzRIK8UsE0OWc1UKk8Nki2PsH1h5==/kisspng-lg-g-watch-r-moto-360-2nd-generation-samsung-gea-luxury-watches-5aad70f759e967.2787900415213160873683.png"
            });
           


        }
       

        
       
    }
}
