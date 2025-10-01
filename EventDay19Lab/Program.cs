using System;

namespace EventDay19Lab
{
    // Delegate
    public delegate string StringHandler(string str);
    internal class Program
    {
        static void Main(string[] args)
        {
            // close
            // closed , closing
            // Delegate pointer to method
            MyString myString = new MyString();
            Console.Write("Enter a string: ");
            string input = Console.ReadLine();

            StringHandler sh0 = delegate (string text) // Anonymous method
            {
                return "You entered: " + text;
            };

            Console.WriteLine(sh0(input)); // Invoke the delegate

            StringHandler sh1 = new StringHandler(myString.CaseText); // Assign method to delegate by new keyword
            Console.WriteLine(sh1(input)); // Invoke the delegate 

            StringHandler sh2 = myString.StringLength; // Another way to assign delegate where new keyword is optional
            Console.WriteLine(sh2(input)); // Invoke the delegate

            StringHandler sh3 = new StringHandler(myString.CaseText);
            Console.WriteLine($"by Invoke Method : {sh3.Invoke("Ahmed Ali")}"); // here we use Invoke method to call the delegate and pass a string directly
            Console.WriteLine(sh3(input));

            //myString.CheckText += new StringHandler(myString.CaseText); // here += is used to subscribe to the event
            // or
            myString.CheckText += myString.StringLength; // here we subscribe to the event without new keyword

            myString.CheckText += MyString_CheckText; // here we subscribe to the event with static method that the program built it when we create the object
        }

        private static string MyString_CheckText(string str)
        {
            return $"Static Method : the string is {str} ";
        }
    }
    class MyString
    {
        // Event : A message sent by an object to signal the occurrence of an action.   
        // The action could be caused by user interaction, such as a mouse click, or it could be triggered by some other program logic.
        // Event can run multiple methods
        public event StringHandler CheckText; // event must be in a class

        public string CaseText(string text)
        {
            if (text == text.ToUpper())
            {
                return ("String is Capitals");
            }
            else if (text == text.ToLower())
            {
                return ("String is Small");
            }
            else
            {
                return ("String is Mixed ");
            }
        }
        public string StringLength(string text)
        {
            return string.Format("the length for text is : {0}", text.Length.ToString());
        }
    }

}

