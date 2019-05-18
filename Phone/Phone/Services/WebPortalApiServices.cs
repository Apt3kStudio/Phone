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

namespace Phone.Services
{
    class WebPortalApiServices
    {
        private static string WebApiBaseURL = "https://apt3kwebportal.azurewebsites.net/";


        internal async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
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

            return response.IsSuccessStatusCode;
        }

        //Todo we need to encode the json to create protection for the password. 
        internal async Task<bool> SigningIn(string email, string password)
        {
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



            return response.IsSuccessStatusCode;
        }
       
    }
}

