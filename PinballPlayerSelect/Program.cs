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
            string tableName = null;
            string emulator = null;
            if (args.Length == 0)
            {
                OutputHelper.ShowMessage("No parameter found, running in testmode (open with \"PinballPlayerSelect <tablename>\" for normal operation)");
                tableName = "test";
            }
            else if (args[0] == "config")
            {
                new Import().ShowDialog();
                return;
            }
            else
            {
                emulator = args[0];
                tableName = args[1];
            }

            Console.WriteLine($"Program started with table '{tableName}' on Emulator '{emulator}'" );
            try
            {
                var config=Config.ReadConfig();

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var specificDisplay = config.Overlays.FirstOrDefault(q => q.Filter == tableName);
                var genericDisplay = config.Overlays.FirstOrDefault(q => q.Filter == null);

                var display = specificDisplay ?? genericDisplay;

                Application.Run(new Launcher(config, emulator, tableName, display));
            }
            catch (Exception ex)
            {
                OutputHelper.ShowMessage(ex.ToString());
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();
    }
}