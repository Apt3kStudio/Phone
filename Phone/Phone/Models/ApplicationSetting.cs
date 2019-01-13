using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Phone.Models
{
   public class ApplicationSetting
    {
       public string tokenString = "Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJQaG9uZUFwcFVzZXIiXSwiQ3VzdG9tX0NsYWltIjoiQ3VzdG9tX0NsYWltX05hbWUiLCJleHAiOjE1NDY3MzA5MzAsImlzcyI6InBob25lYXBwIiwiYXVkIjoicGhvbmVhcHB1c2VycyJ9.Kn9PXHWP2gEOztVGoMYSyopjNgIQgoh-nBHAQPHSygM";

       public void GetJWTUserAcountType(string BearerToken)
        {
            
           
            var jwtEncodedString = tokenString.Substring(7); // trim 'Bearer ' from the start since its just a prefix for the token string

            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            Console.WriteLine("email => " + token.Claims.GetEnumerator().Current.Value);
        }
        /// <summary>
        /// This method saves the account type in the phone local storage
        /// </summary>
        /// <param name="UserAccountType">The type of the account.</param>
        /// <returns>If the account type is successfully stored then the return value is true otherwise the return value is false</returns>
        public bool AddUserAccountTypeToPref(string UserAccountType)
        {
            bool retVal = false;

            SecureStorage.SetAsync("AccountType", UserAccountType).Wait();

            return retVal;

        }
        public async Task<string> GetUserAccountTypePref(string UserAccountType)
        {
            string AccountType = "";

            AccountType = await SecureStorage.GetAsync("AccountType");

            return AccountType;

        }

    }
}
