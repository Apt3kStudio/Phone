using Phone.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Phone.Models
{
    public class ReceiveFromAPI
    {
        public string jwtToken { get; set; }
        public DateTime jwtExpiration { get; set; }
        public string FCMToken { get; set; }   
        /// <summary>
        /// This method save the JwtToken Generated when the user sings in
        /// </summary>
        public void SaveJwtTokenAsync()
        {
            SecureStorage.SetAsync("jwtToken", jwtToken);
        }
        /// <summary>
        /// This method will compare that the FCM token stored on the Web Api is the same stored on the phone local storage
        /// </summary>
        public bool ValidateFCMToken()
        {
            bool doesItMatch = true;
            string storedFCMToken = SecureStorage.GetAsync("FBToken").Result;
            if (storedFCMToken != FCMToken)
                doesItMatch = false;
            return doesItMatch;
        }
       
    }
}
