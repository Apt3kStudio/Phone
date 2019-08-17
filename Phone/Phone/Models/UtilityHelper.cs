using Android.Content;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Phone.Models
{
    public static class UtilityHelper
    {
        public static Guid getNewID()
        {
            Guid id = Guid.NewGuid();
            return id;
        }

        /*TODO: READ DATA METHOD TO READ MESSAGE FROM WATCH WITH TIMESTAMP
         * SAVE LOCAL DEVICE NAME IN DATABASE LOGINUSERVIEWMODEL SAMPLE CODE TYPE SECURESTORAGE
        */

        public static bool doesItExit(string key)
        {
            if(RetrieveFromPhone(key).Result == "")
            {
                return false;
            }
            return true;            
        }
        public static async Task SaveToPhoneAsync(string key, string data)
        {
            await SecureStorage.SetAsync(key, data);
        }
        public static async Task<string> RetrieveFromPhone(string key)
        {
            return await SecureStorage.GetAsync(key);
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static Context GetContext()
        {
            return Android.App.Application.Context;
        }
    }
}
