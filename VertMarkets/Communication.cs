using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VertMarkets.Models;

namespace VertMarkets
{
    public class Communication
    {
        private static string baseUrl = "http://magazinestore.azurewebsites.net/api";
        public static async Task<T> Get<T>(string action)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{baseUrl}{action}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<T> Post<T, Q>(string action, Q data) where Q : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{baseUrl}{action}";
                    var stringContent = new StringContent(JsonConvert.SerializeObject(data), UnicodeEncoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, stringContent);
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
