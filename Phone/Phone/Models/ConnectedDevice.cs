using Android.Bluetooth;
using Android.Content;
using Phone.Droid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Models
{
    public class ConnectedDevice
    {
        public string DeviceName { get; set; }
        public List<string> TimeStamps { get; set; }
        public int CurrentIndex { get; set; }
        public string TimeStamp { get; set; }
        public int ID { get; set; }
        public string GetRSSI()
        {
            return BluetoothDevice.ExtraRssi;
        }
        public int DelayInMilliseconds { get; private set; }
        private Context _context;
        public static void Initialize()
        {
            //TimeStamps = new List<string>();
            //TimeStamps.Add("timestamp1");
            //TimeStamps.Add("timestamp2");
            //TimeStamps.Add("timestamp3");
            //TimeStamps.Add("timestamp4");
            //TimeStamps.Add("timestamp5");
            //TimeStamps.Add("timestamp6");
            //TimeStamps.Add("timestamp7");
            //TimeStamps.Add("timestamp8");
            //TimeStamps.Add("timestamp9");
            //TimeStamps.Add("timestamp10");
        }

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
           await UtilityHelper.SaveToPhoneAsync("stampcounter",  CurrentIndex.ToString());
        }
        public async Task SaveDeviceID()
        {

            await UtilityHelper.SaveToPhoneAsync("DeviceID", ID.ToString());
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
          //  Communicator cmd = new Communicator(_context);
           // cmd.SendMessage("");            
        }
    }
}
