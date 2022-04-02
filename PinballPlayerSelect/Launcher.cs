using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PinballPlayerSelect
{
    public partial class Launcher : Form
    {
        private readonly string _tableName;
        private readonly ConfigValues _config;
        private readonly DisplayGroup _display;
        private int _numPlayers;
        private Monitor _table;
        private Monitor _dmd;
        private Monitor _backglass;

        public Launcher()
        {
            InitializeComponent();
         
        }

        public Launcher(string tableName, ConfigValues config, DisplayGroup display) : this()
        {
            this._tableName = tableName;
            this._config = config;
            this._display = display;
            _numPlayers = _config.Selection.PlayerCountAtStart;
            this.Size = new Size(0, 0);
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            if (_display.Table.Enabled)
            {
                _table = new Monitor(_config.PinballX.Table, _display.Table, _tableName);
                _table.Show();
            }
            if (_display.Dmd.Enabled)
            {
                _dmd = new Monitor(_config.PinballX.Dmd, _display.Dmd, _tableName);
                _dmd.Show();
            }
            if (_display.BackGlass.Enabled)
            {
                _backglass = new Monitor(_config.PinballX.BackGlass, _display.BackGlass, _tableName);
                _backglass.Show();
            }
        }

        private void RedrawPlayerSelect()
        {
            if (_table != null) _table.RedrawSelection(_numPlayers);
            if (_dmd != null) _dmd.RedrawSelection(_numPlayers);
            if (_backglass != null) _backglass.RedrawSelection(_numPlayers);
        }

        private void ExitProgram() {

            this.Close();

        }

        private void LaunchGame() { }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys key);

        private void Launcher_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Selection.MorePlayers)))
            {
                if (_numPlayers < 4)
                {
                    _numPlayers++;
                }
                else if (_config.Selection.Loop)
                {
                    _numPlayers = 1;
                }
                RedrawPlayerSelect();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Selection.LessPlayers)))
            {
                if (_numPlayers > 1)
                {
                    _numPlayers--;
                }
                else if (_config.Selection.Loop)
                {
                    _numPlayers = 4;
                }
                RedrawPlayerSelect();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Selection.StartGame)))
                {
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Selection.ReturnToPinballX)))
            {
                ExitProgram();
            } else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Selection.OnePlayer)))
            {
                _numPlayers = 1;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Selection.TwoPlayers)))
            {
                _numPlayers = 2;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Selection.ThreePlayers)))
            {
                _numPlayers = 3;
                RedrawPlayerSelect();
                LaunchGame();
            }
            else if (Convert.ToBoolean(GetAsyncKeyState((Keys)_config.Selection.FourPlayers)))
            {
                _numPlayers = 4;
                RedrawPlayerSelect();
                LaunchGame();
            }
        }
    }
}
