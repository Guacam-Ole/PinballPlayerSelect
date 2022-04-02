using NLog;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinballPlayerSelect
{
    public partial class Monitor : Form
    {
        private readonly PinballXDisplay _pbxScreenSettings;
        private readonly DisplaySettings _displaySettings;
        private readonly string _tableName;
        private Backgrounds _background;

        public Monitor()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            _background = new Backgrounds();
            
        }

        public Monitor(PinballXDisplay pbxScreenSettings, DisplaySettings displaySettings, string tablename) : this()
        {
            _pbxScreenSettings = pbxScreenSettings;
            _displaySettings = displaySettings;
            _tableName = tablename;
        }

        private void Table_Load(object sender, EventArgs e)
        {
            _background.PaintBackgroundImage(this, _pbxScreenSettings,  _tableName);
            RedrawSelection(1);
            
        }

        public void RedrawSelection(int numberOfPlayers)
        {
            _background.DisplaySelection(this, _displaySettings, numberOfPlayers);
        }
    }
}
