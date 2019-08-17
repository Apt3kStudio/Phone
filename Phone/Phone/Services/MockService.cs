﻿using Phone.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Phone.Services
{
    public class MockService
    {
        private ObservableCollection<RegisteredDevice> RegisteredDevices;

        public MockService()
        {
            RegisteredDevices = new ObservableCollection<RegisteredDevice>();
        }
       public ObservableCollection<RegisteredDevice> MockLoadRegistedDevices()
        {
            Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
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
                    ImageSource = "https://apt3k.azurewebsites.net/images/SmartwatchBatman.png"
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
                    ImageSource = "https://apt3k.azurewebsites.net/images/FossilWomens.png"
                });

                RegisteredDevices.Add(new RegisteredDevice
                {
                    Id = "1",
                    Description = "Jonh Doe's Watch is connected",
                    Text = "Moto Watch",
                    deviceType = "Wear",
                    device = "LRS 365".ToUpper(),
                    deviceName = "LRS",
                    manufacturer = "Motorola",
                    version = "7.0",
                    platform = "Android",
                    idiom = "Watch",
                    Flash = "Flash",
                    Sound = "Sound",
                    Buzz = false,
                    Distance = "25",
                    Measurement = "ft",
                    ImageSource = "https://apt3k.azurewebsites.net/images/Tinkann.png"
                });
            });


            return RegisteredDevices;
        }
    }
}
