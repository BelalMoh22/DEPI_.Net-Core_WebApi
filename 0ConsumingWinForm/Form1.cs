namespace _0ConsumingWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
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
                    dataGridView1.DataSource = result;
                }
            }
        }
    }
    // Deserliaze
    public class DepartmentData
    {
        public int departmentId { get; set; }
        public string name { get; set; }
        public string description { get; set; }


    }
}
