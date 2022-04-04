using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PinballPlayerSelect
{
    public partial class Launcher : Form
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys key);

        private readonly string _tableName;
        private readonly OverlayGroup _overlay;
        private int _numPlayers;
        private Monitor _playfield;
        private Monitor _dmd;
        private Monitor _backglass;

        public Launcher()
        {
            InitializeComponent();
        }

        public Launcher(string tableName, OverlayGroup _overlaysettings) : this()
        {
            _tableName = tableName;
            _overlay = _overlaysettings;
            _numPlayers = Config.Configuration.Input.PlayerCountAtStart;
            Size = new Size(0, 0);
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            if (Config.Configuration.Screens.PlayField.Enabled)
            {
                _playfield = new Monitor(Config.Configuration.Screens.PlayField, _overlay.PlayField, _tableName, Config.Configuration.Media.PlayField, "playfield");
                _playfield.KeyPressed += _screenKeydown;
                _playfield.Show();
            }
            if (Config.Configuration.Screens.Dmd.Enabled)
            {
                _dmd = new Monitor(Config.Configuration.Screens.Dmd, _overlay.Dmd, _tableName, Config.Configuration.Media.Dmd, "dmd");
                _dmd.KeyPressed += _screenKeydown;
                _dmd.Show();
            }
            if (Config.Configuration.Screens.BackGlass.Enabled)
            {
                _backglass = new Monitor(Config.Configuration.Screens.BackGlass, _overlay.BackGlass, _tableName, Config.Configuration.Media.BackGlass, "backglass");
                _backglass.KeyPressed += _screenKeydown;
                _backglass.Show();
            }
        }

        private void RedrawPlayerSelect()
        {
            //Application.DoEvents();
            if (_playfield != null) _playfield.RedrawSelection(_numPlayers);
            if (_dmd != null) _dmd.RedrawSelection(_numPlayers);
            if (_backglass != null) _backglass.RedrawSelection(_numPlayers);
        }

        private void ExitProgram()
        {
            Close();
        }

        private void LaunchGame()
        {
            string target = Path.Combine(Config.Configuration.Launch.WorkingPath, Config.Configuration.Launch.Executable);
            string parameters = Config.Configuration.Launch.Parameters.Replace("[TABLEFILE]", _tableName) + " ";
            switch (_numPlayers)
            {
                case 1:
                    parameters = parameters.Replace("[PLAYER]", Config.Configuration.Launch.OnePlayer);
                    break;

                case 2:
                    parameters = parameters.Replace("[PLAYER]", Config.Configuration.Launch.TwoPlayers);
                    break;

                case 3:
                    parameters = parameters.Replace("[PLAYER]", Config.Configuration.Launch.ThreePlayers);
                    break;

                case 4:
                    parameters = parameters.Replace("[PLAYER]", Config.Configuration.Launch.FourPlayers);
                    break;
            }
            if (_tableName == "test")
            {
                OutputHelper.ShowMessage($"[TEST] The following Command would be run: {target} {parameters}");
            }
            else
            {
                if (Config.Configuration.BatchMode)
                {
                    string batchContents = $"cd \"{Config.Configuration.Launch.WorkingPath}\"\r\n\"{target}\" {parameters}";
                    File.WriteAllText("launch.bat", batchContents);
                }
                else
                {
                    var process = new ProcessStartInfo
                    {
                        Arguments = parameters,
                        FileName = target,
                        WorkingDirectory = Config.Configuration.Launch.WorkingPath
                    };
                    var processInfo = Process.Start(process);
                    if (processInfo != null && !processInfo.HasExited && Config.Configuration.StayOpen)
                    {
                        processInfo.WaitForExit();
                    }
                }
                Close();
            }
        }

        private void _screenKeydown(object sender, KeyEventArgs e)
        {
            if (Convert.ToBoolean(GetAsyncKeyState((Keys)Config.Configuration.Input.MorePlayers)))
            {
                if (_numPlayers < 4)
                {
                    _numPlayers++;
                }
                else if (Config.Configuration.Input.Loop)
                {
                    _numPlayers = 1;
                }
                RedrawPlayerSelect();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)Config.Configuration.Input.LessPlayers)))
            {
                if (_numPlayers > 1)
                {
                    _numPlayers--;
                }
                else if (Config.Configuration.Input.Loop)
                {
                    _numPlayers = 4;
                }
                RedrawPlayerSelect();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)Config.Configuration.Input.StartGame)))
            {
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)Config.Configuration.Input.Exit)))
            {
                ExitProgram();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)Config.Configuration.Input.OnePlayer)))
            {
                _numPlayers = 1;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)Config.Configuration.Input.TwoPlayers)))
            {
                _numPlayers = 2;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)Config.Configuration.Input.ThreePlayers)))
            {
                _numPlayers = 3;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)Config.Configuration.Input.FourPlayers)))
            {
                _numPlayers = 4;
                RedrawPlayerSelect();
                LaunchGame();
            }
        }
    }
}