namespace _0ConsumingConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // First I take object from HTTPClient
            using (HttpClient client = new HttpClient()) 
            {

                // https://localhost:7007/api/departments  Get
                // put to him the uri
                client.BaseAddress = new Uri("https://localhost:7007/");

                // Here will Remove any Requests in the Header
                client.DefaultRequestHeaders.Accept.Clear();
                //Here you apply to him that the data will send to it as json
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Receive Data
                HttpResponseMessage response = client.GetAsync("api/departments").Result;
                //if(response.StatusCode == System.Net.HttpStatusCode.OK ||
                //   response.StatusCode == System.Net.HttpStatusCode.NoContent)
                //{

                //}
                //or 
                if (response.IsSuccessStatusCode)
                {
                    List<DepartmentData> result = await response.Content.ReadAsAsync<List<DepartmentData>>();
                    foreach (DepartmentData item in result) 
                    {
                        Console.WriteLine(item.ToString());
                    }
                    Console.WriteLine("-------------- End Read-----------");
                }
                else
                {
                    Console.WriteLine("Endpoint not running");
                }

                // Get by id
                Console.Write("Enter your ID: ");
                int id;
                int.TryParse(Console.ReadLine(), out id);
                if (id > 0)
                {
                    HttpResponseMessage responseByid = client.GetAsync($"api/departments/{id}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        DepartmentData result = await responseByid.Content.ReadAsAsync<DepartmentData>();
                        Console.WriteLine(result.ToString());
                        Console.WriteLine("-------------- End Read-----------");
                    }
                    else if (responseByid.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Department with {id} not Found");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Id");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Id");
                }

            }

            Console.ReadLine();
        }
    }
    // Deserliaze
    public class DepartmentData
        {
        public int departmentId {  get; set; }
        public string name { get; set; } 
        public string description {  get; set; }

        public override string ToString()
        {
            return $"ID : {departmentId} , Name : {name} , Description : {description}";
        }

    }
}
