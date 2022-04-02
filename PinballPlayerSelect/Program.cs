using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinballPlayerSelect
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {

                Console.WriteLine("Call with \"PinballPlayerSelect <tablename>\"");
                return;
            }
            string tableName = args[0];
            var config = new Config().Configuration;


            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var specificDisplay = config.Displays.FirstOrDefault(q => q.File == tableName);
            var genericDisplay = config.Displays.FirstOrDefault(q => q.File == null);

            var display = specificDisplay ?? genericDisplay;


            Application.Run(new Launcher(tableName, config, display));
        }
    }
}
