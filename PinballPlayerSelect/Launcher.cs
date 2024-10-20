using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PPS
{
    public partial class Launcher : Form
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys key);

        private ConfigValues _config;
        private Emulator _emulator;

        private Overlay _overlay;
        private string _gameParameters;
        private readonly Monitor _dmd;
        private int _numPlayers;
        private readonly ILogger<Launcher> _logger;

        public bool RunInTestMode { get; set; }

        public Launcher(ILogger<Launcher> logger, Monitor dmd) : this()
        {
            _logger = logger;
            _dmd = dmd;
        }

        public Launcher()
        {
            InitializeComponent();
        }

        public void Init(ConfigValues config, string emulator, string tableName, string parameters, Overlay overlaySettings)
        {
            _config = config;
            _emulator = _config.Emulators.FirstOrDefault(q => q.Name == emulator) ?? throw new ArgumentException($"'{emulator}' is not configured. Please be aware that this is case-sensitive");
            _overlay = overlaySettings;
            _numPlayers = config.Input.PlayerCountAtStart;
            _gameParameters = parameters;
            Size = new Size(0, 0);
            DrawDmd(tableName);
            _logger.LogInformation("Initialized launcher with configuration for '{emulator}' and table '{Table}' with parameters '{Parameters}'", emulator, tableName, parameters);
            _logger.LogDebug("config: {Config}", config);
            _logger.LogDebug("overlay: {Overlay}", overlaySettings);
        }

        private void DrawDmd(string tableName)
        {
            _dmd.KeyPressed += _screenKeyDown;
            _dmd.ShowPlayerSelection(_config.Dmd, _overlay, tableName, _emulator.Media, _numPlayers);
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            _dmd.Show();
        }

        private void RedrawPlayerSelect()
        {
            _dmd?.RedrawSelection(_numPlayers);
        }

        private void ExitProgram()
        {
            _logger.LogDebug("Exiting program");
            Close();
        }

        private void LaunchGame()
        {
            string target = Path.Combine(_emulator.WorkingPath, _emulator.Executable);
            string parameters = _gameParameters;

            parameters += " ";

            switch (_numPlayers)
            {
                case 1:
                    parameters = parameters += _emulator.OnePlayer;
                    break;

                case 2:
                    parameters = parameters += _emulator.TwoPlayers;
                    break;

                case 3:
                    parameters = parameters += _emulator.ThreePlayers;
                    break;

                case 4:
                    parameters = parameters += _emulator.FourPlayers;
                    break;
            }
            _logger.LogInformation("Launching '{Game}' with parameters '{Parameters}'", target, parameters);
            if (RunInTestMode)
            {
                OutputHelper.ShowMessage(_logger, $"[TEST] The following Command would be run: '{target} {parameters}'");
            }
            else
            {
                if (_config.BatchMode)
                {
                    string batchContents = $"cd \"{_emulator.WorkingPath}\"\r\n\"{target}\" {parameters}";
                    File.WriteAllText("launch.bat", batchContents);
                }
                else
                {
                    var process = new ProcessStartInfo
                    {
                        Arguments = parameters,
                        FileName = target,
                        WorkingDirectory = _emulator.WorkingPath
                    };
                    var processInfo = Process.Start(process);
                    if (processInfo != null && !processInfo.HasExited && _config.StayOpen)
                    {
                        processInfo.WaitForExit();
                        _logger.LogInformation("External Program has Exited with code '{Result}'", processInfo.ExitCode);
                    }
                }
            }
            Close();
        }

        private void _screenKeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.MorePlayers)))
            {
                if (_numPlayers < 4)
                {
                    _numPlayers++;
                }
                else if (_config.Input.Loop)
                {
                    _numPlayers = 1;
                }
                RedrawPlayerSelect();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.LessPlayers)))
            {
                if (_numPlayers > 1)
                {
                    _numPlayers--;
                }
                else if (_config.Input.Loop)
                {
                    _numPlayers = 4;
                }
                RedrawPlayerSelect();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.StartGame)))
            {
                _logger.LogInformation("Launch Game Key detected");
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.Exit)))
            {
                _logger.LogInformation("Exit Key detected");
                ExitProgram();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.OnePlayer)))
            {
                _logger.LogInformation("1 Player Key detected");
                _numPlayers = 1;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.TwoPlayers)))
            {
                _logger.LogInformation("2 Players Key detected");
                _numPlayers = 2;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.ThreePlayers)))
            {
                _logger.LogInformation("3 Players Key detected");
                _numPlayers = 3;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.FourPlayers)))
            {
                _logger.LogInformation("4 Players Key detected");
                _numPlayers = 4;
                RedrawPlayerSelect();
                LaunchGame();
            }
        }
    }
}