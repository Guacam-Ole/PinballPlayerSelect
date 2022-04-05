using System;
using System.Windows.Forms;

namespace PPS
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