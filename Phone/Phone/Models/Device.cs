using Android.Bluetooth;
using Android.Content;
using Android.Gms.Wearable;
using Phone.Droid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Phone.Models
{
    public class Device
    {
        public string DeviceName { get; set; }
        public List<string> TimeStamps { get; set; }
        public int CurrentIndex { get; set; }
        public string TimeStamp { get; set; }
        public int ID { get; set; }

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

        internal bool InitHandShake(ConnectedDevice connectedwatch)
        {
           return ReceivedHandShake("HandShake");
        }
        private Context _context;
        public string Id { get; set; }
        public string Event { get; set; }
        public string device;
        public string manufacturer;
        public string deviceName;
        public string version;
        public DevicePlatform platform;
        public DeviceIdiom idiom;
        public DeviceType deviceType;
       // private Communicator _communicator;
     //   public void Initialize(Communicator communicator)
       // {
           // _communicator = communicator;
        //}
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
            datamap.PutString("timeStamp", msec.ToString());
            //_communicator.SendStamp(datamap);
        }




      
        public TimeSpan Timer(Action SendMessage)
        {
            Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            SendMessage();
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public string GetRSSI()
        {
            return BluetoothDevice.ExtraRssi;
        }
        public int DelayInMilliseconds { get; private set; }


        internal void SetDelay(int delayInMilliseconds)
        {
            DelayInMilliseconds = delayInMilliseconds;
        }

        public async Task GetPreviousCountAsync()
        {
            await UtilityHelper.RetrieveFromPhone("stampcounter");
            CurrentIndex++;
        }
        public void CounterReset()
        {
            CurrentIndex = 1;
        }
        public async Task SaveCurrentCountAsync()
        {
            await UtilityHelper.SaveToPhoneAsync("stampcounter", CurrentIndex);
        }
        public async Task SaveDeviceID()
        {

            await UtilityHelper.SaveToPhoneAsync("DeviceID", ID);
        }

        internal bool ReceivedHandShake(string message)
        {
            if (message == "HandShake")
            {
                return true;
            }
            return false;
        }

        internal void SendMessage()
        {
            //Communicator cmd = new Communicator(_context);
            //cmd.SendMessage("");
        }
    }
}
