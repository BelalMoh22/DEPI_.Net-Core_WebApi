namespace Day19LabAsyncDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //https://api.github.com/repos/dotnet/runtime
            Console.WriteLine("Starting async method...");

            string url = "https://api.github.com/repos/dotnet/runtime";
            string result = await FetchDataAsync(url);

            Console.WriteLine("Data fetched:");
            Console.WriteLine(result);
        }

        //static  string FetchData(string url)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
        //        HttpResponseMessage response =  client.GetAsync(url);
        //        response.EnsureSuccessStatusCode();
        //        string responseBody = response.Content.ReadAsStringAsync();
        //        return responseBody;
        //    }
        //}

        static async Task<string> FetchDataAsync(string url)  // Task Here means this method is async and will return void
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
    }
}
