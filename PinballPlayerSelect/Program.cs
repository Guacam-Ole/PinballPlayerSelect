using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PPS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            AllocConsole();
            bool runInTestMode = false;
            string tableName = null;
            string parameters = null;
            string emulator = null;
            if (args.Length == 0)
            {
                OutputHelper.ShowMessage("No parameter found, running in testmode");
                runInTestMode = true;
            }
            else if (args[0] == "config")
            {
                new Import().ShowDialog();
                return;
            }
            else if (args.Length < 2)
            {
                throw new ArgumentException("Expected at least two parameters");
            }
            else
            {
                emulator = args[0];
                tableName = args[1];
                parameters = GetParameterString(args);
            }

            Console.WriteLine($"Program started with for table '{tableName}' with parameters '{parameters}' on Emulator '{emulator}'");
            try
            {
                var config = Config.ReadConfig();

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var specificDisplay = config.Overlays.FirstOrDefault(q => q.Filter.Equals(tableName, StringComparison.InvariantCultureIgnoreCase));
                var genericDisplay = config.Overlays.FirstOrDefault(q => q.Filter == null);

                var display = specificDisplay ?? genericDisplay;
                Console.WriteLine($"Using displaySettings with filter '{display.Filter}'");

                Application.Run(new Launcher(config, emulator, tableName, parameters, display));
            }
            catch (Exception ex)
            {
                OutputHelper.ShowMessage(ex.ToString());
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        private static string GetParameterString(string[] parameters)
        {
            return string.Join(" ", parameters[1..]);
        }
    }
}