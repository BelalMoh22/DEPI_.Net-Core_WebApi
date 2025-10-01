namespace Day19EventDemoLab
{
    // Delegate Declaration
    public delegate void HandlerDate(DateTime MyBirthDate);
    public partial class Form1 : Form
    {
        //Event Declaration
        public event HandlerDate GetAge;
        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker1.Value.ToString();
            GetAge(dateTimePicker1.Value);
        }
    }
}
