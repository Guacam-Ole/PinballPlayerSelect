using Accessibility;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Logging;

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace PPS
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            AllocConsole();

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var logger =serviceProvider.GetService<ILogger<Launcher>>();
                

                bool runInTestMode = false;
                string tableName = null;
                string parameters = null;
                string emulator = null;
                if (args.Length == 0)
                {
                    var import = serviceProvider.GetService<Import>();
                    import.ShowDialog();

                    return;
                }
                else if (args.Length < 2)
                {
                    throw new ArgumentException("Expected at least two parameters");
                }
                else if (args[0] == "test")
                {
                    OutputHelper.ShowMessage("Running in test mode (Won't try to open pinball, just display the parameters)");
                    runInTestMode = true;
                    emulator = args[1];
                    tableName = args[2];
                    parameters = GetParameterString(args, 3);
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

                    var specificDisplay = config.Overlays.FirstOrDefault(q => q.Filter != null && q.Filter.Contains(tableName, StringComparison.InvariantCultureIgnoreCase));
                    var genericDisplay = config.Overlays.FirstOrDefault(q => q.Filter == null);

                    var display = specificDisplay ?? genericDisplay;
                    Console.WriteLine($"Using displaySettings with filter '{display.Filter}'");
                    var launcher = serviceProvider.GetService<Launcher>();
                    launcher.RunInTestMode = runInTestMode;
                    launcher.Init(config, emulator, tableName, parameters, display);
                    Application.Run(launcher);
                    
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Critical Error");
                    //var logger = logFactory.CreateLogger<Type>();
                    OutputHelper.ShowMessage("Sorry. Something went wrong. Please check the logs");
                }
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        private static string GetParameterString(string[] parameters, int startParameter = 2)
        {
            return string.Join(" ", parameters[startParameter..]);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<Import>();
            services.AddSingleton<Launcher>();
            services.AddSingleton<Monitor>();
            services.AddSingleton<Backgrounds>();
            services.AddLogging(builder =>
              {
                  builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
                  builder.AddNLog("nlog.config");
              });
        }
    }
}