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
    public partial class Table : Form
    {
        private Config _config;
        private string _tableName;

        public Table()
        {
            _config = new Config();
            InitializeComponent();
        }

        public Table(string tablename) : this()
        {
            _tableName = tablename;
          
        }

        private void RotateImage(ref Image image, int degrees)
        {
            switch (degrees)
            {
                case 90:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 180:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 270:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
        }

        private void LoadBackground()
        {
            this.Location = Screen.AllScreens[_config.Configuration.PinballX.Table.Monitor].WorkingArea.Location;
            this.Left = _config.Configuration.PinballX.Table.X;
            this.Top= _config.Configuration.PinballX.Table.Y;
            this.Width = _config.Configuration.PinballX.Table.Width;
            this.Height = _config.Configuration.PinballX.Table.Height;
            this.BackColor = Color.Green;

            // NO video (yet)
            if (!string.IsNullOrEmpty(_config.Configuration.PinballX.Media.Table.Images))
            {
                var matches=Directory.GetFiles(_config.Configuration.PinballX.Media.Table.Images, $"{_tableName}.*");
                if (matches.Any())
                {
                    string imageFileName = Path.Combine(_config.Configuration.PinballX.Media.Table.Images, matches.First());
                    var image= Image.FromFile(imageFileName);
                    if (_config.Configuration.PinballX.Table.Rotate!=0)
                    {
                        RotateImage(ref image, _config.Configuration.PinballX.Table.Rotate);
                    }
                    BackgroundImage.Image = image;
                    BackgroundImage.Dock = DockStyle.Fill;
                    BackgroundImage.BackColor = Color.Orange;
                }

            }
            
         
        }

        private void Table_Load(object sender, EventArgs e)
        {
            LoadBackground();
        }
    }
}
