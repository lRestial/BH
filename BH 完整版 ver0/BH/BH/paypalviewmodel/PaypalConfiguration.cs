using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BH.paypalviewmodel
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;
        static PaypalConfiguration()
        {
            var config = GetConfig();
            clientId = "AWZQR386vKo_aBxQPDiQxBRZKRqHYM0zqO74fQrZsYxsjqVU1VHtqzk0jwsyMJQFXw1SLPozjbYnWXpe";
            clientSecret = "EKbakGu0rPYnv778yJe6xNjVE1-Xjaom1y5CHboZOCnF_RsAyGWl0dX5rvdhI1Y4TtDaUbWBqOiyw1Wy";
        }
        private static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }
        //create access token
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        //this will return APIContext object
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = GetConfig();
            return apicontext;
        }
    }
}