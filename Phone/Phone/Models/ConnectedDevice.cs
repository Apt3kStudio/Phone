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
