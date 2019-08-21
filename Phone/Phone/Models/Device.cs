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
        public int DelayInMilliseconds { get; private set; }
        public TimeSpan CurrentElapsedTime { get; internal set; }
        public int Proximity { get; set; }
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

        public Device()
        {
            Id = Guid.NewGuid().ToString();
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
        }

        internal void CalculateProximityStatus()
        {
            Task.Run(async () =>
            {
                if (int.Parse(CurrentElapsedTime.ToString()) > await GetPreviousCountAsync())
                {
                    Proximity = (int) ProximityStatus.MovingAway;
                }
                else if (int.Parse(CurrentElapsedTime.ToString()) == await GetPreviousCountAsync())
                {
                    Proximity = (int)ProximityStatus.NotMoving;
                }
                else
                {
                    Proximity = (int)ProximityStatus.MovingCloser;
                }
            }); 
        }

        public TimeSpan Trip(RegisteredDevice watch)
        {
            Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            watch.RandomDelay(); //Todo Send a message to the watch. Make sure to wait for the trip to complete
            stopWatch.Stop();
            TimeSpan timeCaptured = stopWatch.Elapsed;
            return timeCaptured;
        }

        public string GetRSSI()
        {
            return BluetoothDevice.ExtraRssi;
        }

        internal void SetDelay(int delayInMilliseconds)
        {
            DelayInMilliseconds = delayInMilliseconds;
        }

        public async Task<int> GetPreviousCountAsync()
        {
            var timeStamp = await UtilityHelper.RetrieveFromPhone("stampcounter");
            return int.Parse(timeStamp);
        }
        public void CounterReset()
        {
            CurrentIndex = 1;
        }
        public async Task SaveCurrentCountAsync()
        {
            await UtilityHelper.SaveToPhoneAsync("stampcounter", CurrentIndex.ToString());
        }
        public async Task SaveDeviceID()
        {


            await UtilityHelper.SaveToPhoneAsync("DeviceID", ID.ToString());
        }
        internal bool HandShake(RegisteredDevice watch)
        {           
            return watch.HandShake(true);
        }
        public void MainLogic(RegisteredDevice watch)
        {
            if (HandShake(watch))
            {
                Task.Run(async () => { 
                    int i = 0;
                    await UtilityHelper.SaveToPhoneAsync("PreviousTimeStamp", "");
                    await Task.Run(async () =>
                    {
                        while (i < 12000)
                        {
                            CurrentElapsedTime = Trip(watch);

                            var previousTimeStamp = await UtilityHelper.RetrieveFromPhone("PreviousTimeStamp");
                            double millisecs =  CurrentElapsedTime.TotalMilliseconds;
                            if (string.IsNullOrEmpty(previousTimeStamp))
                            {
                                await UtilityHelper.SaveToPhoneAsync("PreviewsTimeStamp", millisecs.ToString());
                            }
                            CalculateProximityStatus();

                            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
                            {
                                watch.Distance =millisecs.ToString();
                            });
                            i++;
                        }
                    });
                });
            }
        }
    }
}
