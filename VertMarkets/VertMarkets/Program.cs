using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using VertMarkets.Models;

namespace VertMarkets
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                MagazineStore m = new MagazineStore();

                // Get Token
                Token t = await m.GetToken();
                Console.WriteLine($"Retrieved Token : {t.token}");
                // Get Subscribers
                var tSubscribers =  m.GetSubscribers(t.token);
                // Get Mazines
                var tMagazines = m.GetMagazines(t.token);
                Task.WaitAll(new Task[] { tSubscribers, tMagazines });
                Subscribers s = tSubscribers.Result;
                var magazines = tMagazines.Result;
                // Get List of Subscribers who subscribed magazine in each category
                var list = m.GetSubscribersForAllCategories(s.data, magazines);
                // Submit the answer and get the response
                AnswerResponse a = await m.PostAnswer(t.token, list);
                Console.WriteLine(JsonConvert.SerializeObject(a));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unhandled Exception : {ex.Message}");
            }
        }
    }
}
