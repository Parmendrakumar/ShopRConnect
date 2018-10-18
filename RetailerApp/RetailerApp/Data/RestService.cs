using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RetailerApp.Data
{
   public class RestService
    {
        public static async Task<string> PostRegistration(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Registration/Sinup?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> RegistrationOTP(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Registration/OTP?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> RegistrationSetPassword(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Registration/Password?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> RegistrationOtherDetail(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Registration/OtherDetail?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> Login(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> UploadOffer(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login/SaveOffer?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }

        public static async Task<string> GetOffers(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login/GetOffer?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> GetQuery(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login/GetQuery?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }

        public static async Task<string> ResendOTP(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Registration/ResendOTP?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> ForgotPassword(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Registration/ForGotPassword?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }

        public static async Task<string> VerificationCode(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/GoogleVerification/FetchCode?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> VerificationInsert(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/GoogleVerification/InsertCode?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }

        public static async Task<string> SaveChat(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login/SaveChat?{inputs}";
            StringContent queryString = new StringContent("");
           
            var response = await client.PostAsync(url, queryString);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> GetChat(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login/GetChat?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> GetStoreDetail(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login/GetStoreDetail?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);
            var airportJson = response.Content.ReadAsStringAsync().Result;
            return airportJson;
        }
        public static async Task<string> GetStateCity(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            // client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            // client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var url = $"http://maudit.elixirct.net/Uploadfiletoserver/api/FetchStateAndCity?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);

            var airportJson = response.Content.ReadAsStringAsync().Result;
            // client.CL
            return airportJson;
        }
        public static async Task<string> GetStateCity1(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            // client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            // client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login/GetStateAndCity?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);

            var airportJson = response.Content.ReadAsStringAsync().Result;
            // client.CL
            return airportJson;
        }
        public static async Task<string> GetCity(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            // client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            // client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var url = $"http://maudit.elixirct.net/Uploadfiletoserver/api/FetchStateAndCity?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);

            var airportJson = response.Content.ReadAsStringAsync().Result;
            // client.CL
            return airportJson;
        }
        public static async Task<string> GetCity1(string inputs)
        {
            var client = new System.Net.Http.HttpClient();
            // client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            // client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            var url = $"http://elixirct.in/ShopRConservicePublish/api/Login/GetStateAndCity?{inputs}";
            StringContent queryString = new StringContent("");
            var response = await client.GetAsync(url);

            var airportJson = response.Content.ReadAsStringAsync().Result;
            // client.CL
            return airportJson;
        }
    }
}
