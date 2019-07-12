using Android.Gms.Wearable;
using Phone.Droid;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Phone.Models
{
    public class Device
    {
        public Device()
        {
            Id = Guid.NewGuid().ToString();
            // Device Model (SMG-950U, iPhone10,6)
            device = DeviceInfo.Model;
            // Manufacturer (Samsung)
            manufacturer = DeviceInfo.Manufacturer;
            // Device Name (Motz's iPhone)
            deviceName = DeviceInfo.Name;
            // Operating System Version Number (7.0)
            version = DeviceInfo.VersionString;
            // Platform (Android)
            platform = DeviceInfo.Platform;
            // Idiom (Phone)
            idiom = DeviceInfo.Idiom;
            // Device Type (Physical)
            deviceType = DeviceInfo.DeviceType;
        }
        public string Id { get; set; }
        public string Event { get; set; }
        public string device;
        public string manufacturer;
        public string deviceName;
        public string version;
        public DevicePlatform platform;
        public DeviceIdiom idiom;
        public DeviceType deviceType;
        public string TimeStamp { get; set; }
        private Communicator _communicator;
        public void Initialize(Communicator communicator)
        {
            _communicator = communicator;
        }
        public void AdvertiseMyself(bool enable)
        {
            if (enable)
            {
                SendTimeStamp();
            }
        }
        public void SendTimeStamp()
        {
            int msec = DateTime.Now.Millisecond;
            DataMap datamap = new DataMap();
            datamap.PutString("TimeStamp", msec.ToString());
            _communicator.SendStamp(datamap);
        }
    }
}
