using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Day19EventDemoLab
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.GetAge += F1_GetAge;
            f1.Show();
        }

        private void F1_GetAge(DateTime MyBirthDate)
        {
            label1.Text = (DateTime.Now.Year - MyBirthDate.Year).ToString();
        }

        private void WriteNumber(Control sender)
        {
            //if (sender is Button btn)
            //{
            //    label2.Text += btn.Text;
            //}
            // or
            label2.Text = "";
            label2.Text += (sender as Button).Text;
        }

        private void button_Click(object sender, EventArgs e)
        {
            WriteNumber(sender as Button);
        }
    }
}
