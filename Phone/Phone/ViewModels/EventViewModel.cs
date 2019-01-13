using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Phone.Models;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Net.Mail;
using System.Net;

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
        public async Task PlaySound()
        {
            try
            {
                var assembly = typeof(App).GetTypeInfo().Assembly;
                Stream audioStream = assembly.GetManifestResourceStream("Phone.Droid.Resources.fire_truck.wav");
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load(audioStream);
                player.Play();
            }
            catch (Exception ex)
            {
                // Unable to turn on/off flashlight
            }

        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }

        public async Task SendEmailViaSMTP(string subject, string body, List<string> recipients)
        {
            try
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp.gmail.com", 587);
                mail.From = new MailAddress("developer@apt3k.com");
                foreach (var recipient in recipients)
                {
                    mail.To.Add(recipient);
                }
                mail.Subject = subject;
                mail.Body = body;
                smtpServer.Credentials = new NetworkCredential("developer@apt3k.com", "Dmc10040");
                smtpServer.UseDefaultCredentials = false;
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

            }
        }

        public async Task SendSms(string messageText, List<string> recipients)
        {
            try
            {
                var message = new SmsMessage(messageText, recipients);
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }


        }

    }
}
