using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PinballPlayerSelect
{
    public partial class Monitor : Form
    {
        private readonly Screen _screenSettings;
        private readonly Overlay _overlaySettings;
        private readonly string _tableName;
        private readonly string _imagepath;
        private Backgrounds _background;
        public event EventHandler<KeyEventArgs> KeyPressed;


        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public Monitor()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            _background = new Backgrounds();
        }

        public Monitor(Screen pbxScreenSettings, Overlay overlaySettings, string tablename, string imagepath, string tag) : this()
        {
            _screenSettings = pbxScreenSettings;
            _overlaySettings = overlaySettings;
            _tableName = tablename;
            _imagepath = imagepath;
            Tag = tag;
        }

        private void OnTop()
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        private void Monitor_Load(object sender, EventArgs e)
        {
            if (_screenSettings.OnTop) OnTop();
            Backgrounds.PaintBackgroundImage(this, _screenSettings, _imagepath, _tableName);
            RedrawSelection(1);
            if (_tableName == "test") TestMode();
        }

        private void TestMode()
        {
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            coordsInfo.Visible = true;
            PlayerNum.Visible = false;
            this.SizeChanged += ShowCoords;
            this.Move += ShowCoords;
            ShowCoords(null,null);
        }

        private void ShowCoords(object sender, EventArgs e)
        {
            string txt= $" X:{this.Left} Y:{this.Top} Width: {this.Width} Height: {this.Height}\n\nAvailable Screens:\n";

            int counter = 0;
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                txt += $"screen {counter} {screen.DeviceName},X:{screen.WorkingArea.Left} Y:{screen.WorkingArea.Top} Width: {screen.WorkingArea.Width} height: {screen.WorkingArea.Height}\n";
                counter++;
            }
            coordsInfo.Text = txt;
        }

        public void RedrawSelection(int numberOfPlayers)
        {
            _background.DisplaySelection(this, _screenSettings, _overlaySettings, numberOfPlayers);
        }

        private void Monitor_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPressed(this,e);
        }
    }
}