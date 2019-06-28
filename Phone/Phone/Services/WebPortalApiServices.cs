﻿using Newtonsoft.Json;
using Phone.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Essentials;
using System.IO;
using Xamarin.Forms;
namespace Phone.Services
{
    class WebPortalApiServices
    {

        private static string WebApiBaseURL = "https://apt3kwebportal.azurewebsites.net/";

        public WebPortalApiServices()
        {            
            #if DEBUG
                WebApiBaseURL = "http://192.168.1.168:45455/";
            #else
                WebApiBaseURL = "https://apt3kwebportal.azurewebsites.net/";
            #endif
            WebApiBaseURL = "https://apt3kwebportal.azurewebsites.net/";
        }

        internal async Task<Message> RegisterAsync(string email, string password, string confirmPassword)
        {
            if (!UtilityHelper.IsValidEmail(email))
            {
                return new Message("Email Not Valid", "Email Not Valid", false);
               
            }
            var client = new HttpClient();
            var model = new Registration
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(WebApiBaseURL +"api/auth/registration", httpContent);

            
            return new Message("Registration", email + " is registered!",response.IsSuccessStatusCode);
        }

        //Todo we need to encode the json to create protection for the password. 
        internal async Task<Message> SigningIn(string email, string password)
        {
            if (!UtilityHelper.IsValidEmail(email))
            {
                return new Message("Email Not Valid", "Email Not Valid", false);

            }
            var client = new HttpClient();
            var model = new Registration
            {
                Email = email,
                Password = password,
                FBToken = SecureStorage.GetAsync("FBToken").Result
            };    
            var json = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(WebApiBaseURL + "api/auth/Login", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Stream receiveStream = response.Content.ReadAsStreamAsync().Result;
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string theContent = readStream.ReadToEnd();
                ReceiveFromAPI rfp = JsonConvert.DeserializeObject<ReceiveFromAPI>(theContent);
                await Task.Run(() => 
                {
                    rfp.SaveJwtTokenAsync();                   
                });
                await Task.Run(async () =>
                {
                    bool isAmatch = rfp.ValidateFCMToken();
                    if (!isAmatch)
                    {
                        await SendFCMTokenAsync();
                    } 
                });
            }
            return new Message("Sign In", "Welcome "+ email + "!", response.IsSuccessStatusCode);
        }
        internal async Task<bool> SendFCMTokenAsync()
        {
            var client = new HttpClient();
            var model = new Registration
            {
                Email = SecureStorage.GetAsync("Email").Result,
                Password = "dsdsds",
                FBToken = SecureStorage.GetAsync("FBToken").Result
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(WebApiBaseURL + "api/auth/ChangeFCMToken", httpContent);



            return response.IsSuccessStatusCode;
        }

    }
}

