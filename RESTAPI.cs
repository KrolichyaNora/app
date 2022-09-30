using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace app
{
    internal class RESTAPI : IStorage
    {
        // TODO: Static class?
        // We shouldn't create a new client for every page, there is should be only one for whole app.
        HttpClient client;
        public RESTAPI() { 
            client = new HttpClient();
            // Do version check?
        }

        public async Task<User?> Auth(User user)
        {
            string jdata = JsonSerializer.Serialize(user);
            StringContent content = new StringContent(jdata);
            HttpResponseMessage resp = await client.PostAsync(Config.API_URL + "/login", content);
            User res = await resp.Content.ReadFromJsonAsync<User>();
            if (res.id == -1) return null;
            return res;
        }

        public async Task<User> Register(User user)
        {
            // TODO: Add registration
            throw new NotImplementedException();
        }
    }
}
