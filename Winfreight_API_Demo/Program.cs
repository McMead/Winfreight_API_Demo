using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Winfreight_API_Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();

            var request_auth = new HttpRequestMessage(HttpMethod.Post, "{base url}/Winfreight_API/login?username={username}&password={password}");

            request_auth.Headers.Accept.Clear();

            request_auth.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await client.SendAsync(request_auth, CancellationToken.None);
           
            var request_call = new HttpRequestMessage(HttpMethod.Post, "{base url}/Winfreight_API/gettracking");

            Params param = new Params();

            param.Waybill = "310537";

            param.GroupName = "ac";

            var json = JsonConvert.SerializeObject(param);

            request_call.Content = new StringContent(json, Encoding.UTF8, "application/json");           

            var response = client.SendAsync(request_call, CancellationToken.None);

            string responseBody = await response.Result.Content.ReadAsStringAsync();

            Console.WriteLine(responseBody);

            Console.ReadLine();
        }

        class Params
        {
            public string Waybill { get; set; }
            public string GroupName { get; set; }
        }
    }


}
