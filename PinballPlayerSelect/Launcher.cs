﻿using System;
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

        private readonly ConfigValues _config;
        private readonly Emulator _emulator;
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

        public Launcher(ConfigValues config, string emulator, string tableName, OverlayGroup overlaysettings) : this()
        {
            _config = config;
            _emulator = _config.Emulators.FirstOrDefault(q => q.Name == emulator) ?? throw new ArgumentException($"'{emulator}' is not configured. Please be aware that this is case-sensitive");
            _tableName = tableName;
            _overlay = overlaysettings;
            _numPlayers = config.Input.PlayerCountAtStart;
            Size = new Size(0, 0);
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            if (_config.Screens.PlayField.Enabled)
            {
                _playfield = new Monitor( _config.Screens.PlayField, _overlay.PlayField, _tableName, _emulator.Media.PlayField, "playfield");
                _playfield.KeyPressed += _screenKeydown;
                _playfield.Show();
            }
            if (_config.Screens.Dmd.Enabled)
            {
                _dmd = new Monitor( _config.Screens.Dmd, _overlay.Dmd, _tableName, _emulator.Media.Dmd, "dmd");
                _dmd.KeyPressed += _screenKeydown;
                _dmd.Show();
            }
            if (_config.Screens.BackGlass.Enabled)
            {
                _backglass = new Monitor( _config.Screens.BackGlass, _overlay.BackGlass, _tableName, _emulator.Media.BackGlass, "backglass");
                _backglass.KeyPressed += _screenKeydown;
                _backglass.Show();
            }
        }

        private void RedrawPlayerSelect()
        {
            _playfield?.RedrawSelection(_numPlayers);
            _dmd?.RedrawSelection(_numPlayers);
            _backglass?.RedrawSelection(_numPlayers);
        }

        private void ExitProgram()
        {
            Close();
        }

        private void LaunchGame()
        {
            string target = Path.Combine(_emulator.WorkingPath, _emulator.Executable);
            string parameters = _emulator.Parameters.Replace("[TABLEFILE]", _tableName) + " ";
            switch (_numPlayers)
            {
                case 1:
                    parameters = parameters.Replace("[PLAYER]", _emulator.OnePlayer);
                    break;

                case 2:
                    parameters = parameters.Replace("[PLAYER]", _emulator.TwoPlayers);
                    break;

                case 3:
                    parameters = parameters.Replace("[PLAYER]", _emulator.ThreePlayers);
                    break;

                case 4:
                    parameters = parameters.Replace("[PLAYER]", _emulator.FourPlayers);
                    break;
            }
            if (_tableName == "test")
            {
                OutputHelper.ShowMessage($"[TEST] The following Command would be run: {target} {parameters}");
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
                    }
                }
                Close();
            }
        }

        private void _screenKeydown(object sender, KeyEventArgs e)
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
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.Exit)))
            {
                ExitProgram();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.OnePlayer)))
            {
                _numPlayers = 1;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.TwoPlayers)))
            {
                _numPlayers = 2;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.ThreePlayers)))
            {
                _numPlayers = 3;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Input.FourPlayers)))
            {
                _numPlayers = 4;
                RedrawPlayerSelect();
                LaunchGame();
            }
        }
    }
}