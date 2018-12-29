using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Phone.Models;
using System.Threading.Tasks;
namespace Phone.ViewModels
{
    public class EventViewModel : IEventModel
    {
        public string EventID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string EventName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string EventMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string EventDuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string VibrateMe(int inDuration = 3)
    {
            string isVibrate ="";
            try
            {
                
                Vibration.Vibrate();
                var duration = TimeSpan.FromSeconds(inDuration);
                Vibration.Vibrate(duration);
                isVibrate = "Success";
            }
            catch (FeatureNotSupportedException ex)
            {
                isVibrate = "Not Supported";
            }
            catch (Exception ex)
            {
                isVibrate = "Error";
            }
            return isVibrate;

        }
        public void CancelVibration()
        {
            try
            {
                Vibration.Cancel();
            }
            catch (FeatureNotSupportedException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }
        public async Task FlashLighOnAsync()
        {
            try
            {
                // Turn On
                await Flashlight.TurnOnAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to turn on/off flashlight
            }
        }

        public async Task FlashLighOffAsync()
        {
            try
            { 
                // Turn Off
                await Flashlight.TurnOffAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to turn on/off flashlight
            }
        }
    }
}
