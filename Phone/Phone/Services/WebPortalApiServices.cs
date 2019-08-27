using Newtonsoft.Json;
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
using System.Net.Http.Headers;

namespace Phone.Services
{
    class WebPortalApiServices
    {

        private static string WebApiBaseURL = "https://apt3kwebportal.azurewebsites.net";

        public WebPortalApiServices()
        {
#if DEBUG
           
WebApiBaseURL = "https://192.168.1.168:5001";
#else
WebApiBaseURL = "https://apt3k.azurewebsites.net"; // Prod
#endif
            
        }
        internal async Task<Message> SigningIn(string email, string password)
        {
            if (!UtilityHelper.IsValidEmail(email))
            {
                return new Message("Email Not Valid", "Email Not Valid", false);

            }
            Models.Device device = new Models.Device();

            var client = new HttpClient(new System.Net.Http.HttpClientHandler());
            var model = new Registration
            {
                Email = email,
                Password = password,
                FBToken = SecureStorage.GetAsync("FBToken").Result,
                DeviceName = device.deviceName

            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            try
            {
                var response = await client.PostAsync(WebApiBaseURL + "/api/auth/Login", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    ReceiveFromAPI apiResult = new ReceiveFromAPI();
                    apiResult = ParseResult(response, apiResult);                    
                    await Task.Run(() =>
                    {
                        apiResult.SaveJwtTokenAsync();
                    });
                    await Task.Run(async () =>
                    {
                        bool isAmatch = apiResult.ValidateFCMToken();
                        if (!isAmatch)
                        {
                            await SendFCMTokenAsync();
                        }
                    });
                }
                return new Message("Sign In", "Welcome " + email + "!", response.IsSuccessStatusCode);
            }
            catch (Exception e)
            {
                return new Message("Error", "Error " + email + "!", false);

                // throw;
            }


        }

        private static T ParseResult<T>(HttpResponseMessage response, T model)
        {
            Stream receiveStream = response.Content.ReadAsStreamAsync().Result;
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string theContent = readStream.ReadToEnd();
            model  = JsonConvert.DeserializeObject<T>(theContent);
            return model;
        }

        internal async Task<Message> RegisterAsync(string email, string password, string confirmPassword)
        {
            if (!UtilityHelper.IsValidEmail(email))
            {
                return new Message("Email Not Valid", "Email Not Valid", false);

            }
            var client = new HttpClient(new System.Net.Http.HttpClientHandler());
            var model = new Registration
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            try
            {


                var response = await client.PostAsync(WebApiBaseURL + "/api/auth/registration", httpContent);

                return new Message("Registration", email + " is registered!", response.IsSuccessStatusCode);
            }
            catch (Exception e)
            {
                return new Message("Registration", email + " is registered!", false);
                //throw;
            }
        }

        internal async Task<bool> SendFCMTokenAsync()
        {
            var client = new HttpClient();
            Models.Device device = new Models.Device();
            var model = new Registration
            {
                Email = SecureStorage.GetAsync("Email").Result,
                Password = "dsdsds",
                FBToken = SecureStorage.GetAsync("FBToken").Result,
                DeviceName = device.deviceName
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(WebApiBaseURL + "/api/auth/ChangeFCMToken", httpContent);



            return response.IsSuccessStatusCode;
        }
        internal async Task<bool> GetRegisteredDevices()
        {
            try
            {
                var client = new HttpClient();
                Models.Device device = new Models.Device();
                var id = new { id = "1" };

                var json = JsonConvert.SerializeObject(id);

                var response = await client.GetAsync(WebApiBaseURL + "/api/Values");

                HttpContent httpContent = new StringContent("");
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
               // var response = await client.PostAsync($"{WebApiBaseURL}/api/auth/token", httpContent);



                Stream receiveStream = response.Content.ReadAsStreamAsync().Result;
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var theContent = readStream.ReadToEnd();
                var jresult = JsonConvert.DeserializeObject<string>(theContent);

                string BearerToken = "";
                //BearerToken = ParseResult(Reponse, BearerToken);

               

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
              //  var response = await client.GetAsync(WebApiBaseURL + "api/Values/");
                return response.IsSuccessStatusCode;
            }
            catch (Exception EX)
            {
                return false;
             
            }

        }
    }
}

