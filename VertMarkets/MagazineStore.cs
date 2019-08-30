using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VertMarkets.Models;
using System.Linq;
namespace VertMarkets
{
    public class MagazineStore
    {
        public async Task<Token> GetToken()
        {
            string action = "/token";
            return await Communication.Get<Token>(action);
        }
        public async Task<Subscribers> GetSubscribers(string token)
        {
            string action = $"/subscribers/{token}";
            return await Communication.Get<Subscribers>(action);
        }

        private async Task<Categories> GetCategories(string token)
        {
            string action = $"/categories/{token}";
            return await Communication.Get<Categories>(action);
        }
        public async Task<Dictionary<string, List<Magazine>>> GetMagazines(string token)
        {
            Categories c = await GetCategories(token);
            List<Task<Magazines>> lTasks = new List<Task<Magazines>>();
            Dictionary<string, List<Magazine>> magazines = new Dictionary<string, List<Magazine>>();
            foreach(var cat in c.Data)
            {
                string action = $"/magazines/{token}/{cat}";
                var task =  Communication.Get<Magazines>(action);
                lTasks.Add(task);
            }
            await Task.WhenAll(lTasks.ToArray());
            foreach(var t in lTasks)
            {
                magazines.Add(t.Result.data.First().category, t.Result.data);
            }
            return magazines;
        }
        public async Task<AnswerResponse> PostAnswer(string token, string[] subscribers)
        {
            AnswerRequest request = new AnswerRequest { subscribers = subscribers };
            string action = $"/answer/{token}";
            return await Communication.Post<AnswerResponse, AnswerRequest>(action, request);
        }

        public string[] GetSubscribersForAllCategories(List<Subscriber> subscribers, Dictionary<string, List<Magazine>> magazines )
        {
            List<string> l = new List<string>();
            foreach (var item in subscribers)
            {
                int counter = 0;
                foreach (var items in magazines)
                {
                    if(items.Value.Select(x => x.id).Intersect(item.magazineIds).Any())
                        counter++;
                }
                if (counter == magazines.Count())
                    l.Add(item.id);
            }

            return l.ToArray();
        }

        
    }
}
