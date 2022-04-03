using System;
using System.Windows.Forms;

namespace PinballPlayerSelect
{
    public static class OutputHelper
    {
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
            MessageBox.Show(message);
        }
    }
}