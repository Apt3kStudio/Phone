using Phone.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Services
{
    public class MockService
    {
        public MockService()
        {
           // RegisteredDevices = new ObservableCollection<RegisteredDevice>();
        }
        public  async Task MockLoadRegistedDevices(ObservableCollection<RegisteredDevice>  RegisteredDevices, ObservableCollection<RegisteredDevice> UnRegisteredDevices, string Distance, int NumberOfDevices = 3 )
        {
         
                for (var i = 1; i <= NumberOfDevices; i++)
                {
                    switch (i)
                    {
                        case 1:
                            if (!UtilityHelper.doesItExit("OnePlus 5"))
                            {

                            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
                            {
                                UnRegisteredDevices.Add(new RegisteredDevice
                                {
                                    Id = NumberOfDevices.ToString(),
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
                                    Distance = Distance,
                                    Measurement = "ft",
                                    ImageSource = "https://apt3k.azurewebsites.net/images/SmartwatchBatman.png"
                                });
                            });
                            }
                            else
                            {
                            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
                            {
                                RegisteredDevices.Add(new RegisteredDevice
                                {
                                    Id = NumberOfDevices.ToString(),
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
                                    Distance = Distance,
                                    Measurement = "ft",
                                    ImageSource = "https://apt3k.azurewebsites.net/images/SmartwatchBatman.png"
                                });
                            });
                        }
                            break;
                        case 2:
                            if (!UtilityHelper.doesItExit("Galaxy Watch"))
                            {
                            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
                            {
                                UnRegisteredDevices.Add(new RegisteredDevice
                                {
                                    Id = "1",
                                    Description = "Micheal Smith's watch is Connected",
                                    Text = "Samsung Watch",
                                    deviceType = "Wear",
                                    device = "Galaxy Watch".ToUpper(),
                                    deviceName = "Galaxy Watch",
                                    manufacturer = "Samsung",
                                    version = "10.0",
                                    platform = "Android",
                                    idiom = "Watch",
                                    Flash = "Flash",
                                    Sound = "Sound",
                                    Buzz = false,
                                    Distance = Distance,
                                    Measurement = "ft",
                                    ImageSource = "https://apt3k.azurewebsites.net/images/FossilWomens.png"
                                });
                            });
                        }
                            else
                            {
                                await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
                                {
                                    RegisteredDevices.Add(new RegisteredDevice
                                {
                                    Id = "1",
                                    Description = "Micheal Smith's watch is Connected",
                                    Text = "Samsung Watch",
                                    deviceType = "Wear",
                                    device = "Galaxy Watch".ToUpper(),
                                    deviceName = "Galaxy Watch",
                                    manufacturer = "Samsung",
                                    version = "10.0",
                                    platform = "Android",
                                    idiom = "Watch",
                                    Flash = "Flash",
                                    Sound = "Sound",
                                    Buzz = false,
                                    Distance = Distance,
                                    Measurement = "ft",
                                    ImageSource = "https://apt3k.azurewebsites.net/images/FossilWomens.png"
                                });
                                });
                        }
                            break;
                        case 3:
                            if (!UtilityHelper.doesItExit("LRS 365"))
                            {
                            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
                            {
                                UnRegisteredDevices.Add(new RegisteredDevice
                                {
                                    Id = "1",
                                    Description = "Jonh Doe's Watch is connected",
                                    Text = "Moto Watch",
                                    deviceType = "Wear",
                                    device = "LRS 365".ToUpper(),
                                    deviceName = "LRS 365",
                                    manufacturer = "Motorola",
                                    version = "7.0",
                                    platform = "Android",
                                    idiom = "Watch",
                                    Flash = "Flash",
                                    Sound = "Sound",
                                    Buzz = false,
                                    Distance = Distance,
                                    Measurement = "ft",
                                    ImageSource = "https://apt3k.azurewebsites.net/images/Tinkann.png"
                                });
                            });
                        }
                            else
                            {
                                await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
                                {
                                    RegisteredDevices.Add(new RegisteredDevice
                                {
                                    Id = "1",
                                    Description = "Jonh Doe's Watch is connected",
                                    Text = "Moto Watch",
                                    deviceType = "Wear",
                                    device = "LRS 365".ToUpper(),
                                    deviceName = "LRS 365",
                                    manufacturer = "Motorola",
                                    version = "7.0",
                                    platform = "Android",
                                    idiom = "Watch",
                                    Flash = "Flash",
                                    Sound = "Sound",
                                    Buzz = false,
                                    Distance = Distance,
                                    Measurement = "ft",
                                    ImageSource = "https://apt3k.azurewebsites.net/images/Tinkann.png"
                                });
                                });
                        }
                            break;
                    }
                }
           
        }
    }
}
