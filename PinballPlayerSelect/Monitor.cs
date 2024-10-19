using Microsoft.Extensions.Logging;

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PPS
{
    public partial class Monitor : Form
    {
        private Screen _screenSettings;
        private Overlay _overlaySettings;
        private string _tableName;
        private string _imagePath;
        private int _numberOfPlayers;
        private readonly Backgrounds _background;

        public event EventHandler<KeyEventArgs> KeyPressed;

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private readonly ILogger<Monitor> _logger;
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public Monitor(ILogger<Monitor> logger) : this()
        {
            _logger = logger;
        }

        public Monitor()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            _background = new Backgrounds();
        }

        private void OnTop()
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        public void ShowPlayerSelection(Screen screenSettings, Overlay overlaySettings, string tableName, string imagePath, int numberOfPlayers)
        {
            _screenSettings = screenSettings;
            _overlaySettings = overlaySettings;
            _tableName = tableName;
            _imagePath = imagePath;
            _numberOfPlayers = numberOfPlayers;
            _logger.LogDebug("Showing PlayerSelection for Table '{Table}' ", tableName);
        }

        private void Monitor_Load(object sender, EventArgs e)
        {
            if (_screenSettings.OnTop) OnTop();
            _background.PaintBackgroundImage(this, _screenSettings, _imagePath, _tableName);
            RedrawSelection(_numberOfPlayers);
            if (_tableName == "test") TestMode();
        }

        private void TestMode()
        {
            _logger.LogInformation("Working in Testmode");
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            coordsInfo.Visible = true;
            PlayerNum.Visible = false;
            this.SizeChanged += ShowCoordinates;
            this.Move += ShowCoordinates;
            ShowCoordinates(null, null);
        }

        private void ShowCoordinates(object sender, EventArgs e)
        {
            string txt = $" X:{this.Left} Y:{this.Top} Width: {this.Width} Height: {this.Height}\n\nAvailable Screens:\n\n\n";

            int counter = 0;
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                txt += $"screen {counter} X:{screen.WorkingArea.Left} Y:{screen.WorkingArea.Top} Width: {screen.WorkingArea.Width} height: {screen.WorkingArea.Height} [{screen.DeviceName}] \n";
                txt += $"(relative pos): X:{Left - screen.WorkingArea.Left} Y:{Top - screen.WorkingArea.Top}\n\n";
                counter++;
            }
            coordsInfo.Text = txt;
            _logger.LogInformation("Coordinates:" + txt);
        }

        public void RedrawSelection(int numberOfPlayers)
        {
            _background.DisplaySelection(this, _screenSettings, _overlaySettings, numberOfPlayers);
            _logger.LogInformation("Redrawn PlayerSelect with '{Count}' players", numberOfPlayers);
        }

        private void Monitor_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressed(this, e);
        }
    }
}