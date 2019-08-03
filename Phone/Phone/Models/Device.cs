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

        public TimeSpan CurrentElapse { get; set; }
        public int ProxStatus { get; set; }

        public TimeSpan CurrentTimeStamp { get; set; }
        public int PreviousTimeStamp { get; set; }
        public bool isFriend { get; set; }

        private ConnectedDevice _connectedDevice;

        public Device()
        {
            //Initialize timestamp for first time it run
            CurrentElapse = TimeSpan.Zero;
            SaveCurrentTimeStamp();
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

        public void Initialize(Communicator communicator, ConnectedDevice connectedDevice)
        {
            _communicator = communicator;
            _connectedDevice = connectedDevice;
        }
        public int InitiateProximity()
        {
            _communicator.SendHadShake();
            isFriend = _communicator.HadShakeReceived();
            if (isFriend)
            {
                CurrentElapse = TimeStampTimer(_communicator.StartTrip());
                CalculateProximity(CurrentElapse);
            }
            int result = 0;
            return result;
        }

        public void CalculateProximity( TimeSpan CurrentTimeStamp)
        {
            int PreviousTS = GetPreviousTimeStamp();
            if (CurrentTimeStamp.TotalMilliseconds > PreviousTS) {
                ProxStatus = (int)ProximityStatus.MovingAway;
            }
            else if (CurrentElapse.TotalMilliseconds == PreviousTS) {
                ProxStatus = (int)ProximityStatus.NotMoving;
            }
            else
            {
                ProxStatus = (int)ProximityStatus.MovingCloser;
            }
            //int intTimeDifference = CurrentTimeStamp - PreviousTimeStamp;
            SaveCurrentTimeStamp();

        }

        public void SaveCurrentTimeStamp()
        {
            Task.Run(async () => {
                await UtilityHelper.SaveToPhoneAsync("previoustimestamp", CurrentElapse.TotalMilliseconds.ToString());
            });
        }

        public TimeSpan TimeStampTimer(Action func)
        {
            Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            func();
            stopWatch.Stop();
            return stopWatch.Elapsed;

        }

        public int GetPreviousTimeStamp()
        {            
                return int.Parse(UtilityHelper.RetrieveFromPhone("previoustimestamp").Result);
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
            datamap.PutString("timeStamp", msec.ToString());
            //_communicator.SendStamp(datamap);
        }

        public static explicit operator Device(DataMap v)
        {
            throw new NotImplementedException();
        }
    }
}
