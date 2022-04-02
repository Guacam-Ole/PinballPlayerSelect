using NLog;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinballPlayerSelect
{
    public class Backgrounds
    {
        public enum Screens
        {
            Dmd,
            Backglass,
            Playfield
        }

        private readonly ILogger _logger;
        private readonly ConfigValues _config;

        public Backgrounds()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _config = new Config().Configuration;
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


        public void PaintBackgroundImage(Form form, PinballXDisplay pbxDisplaySettings, string tablename)
        {
         //   var background = form.BackgroundImage;
            //var background = form.Controls.OfType<PictureBox>().FirstOrDefault(q => q.Tag.Equals("Background"));
            //if (background == null) throw new Exception("PictureBox not found");

            form.Location = Screen.AllScreens[pbxDisplaySettings.Monitor].WorkingArea.Location;
            form.Left = pbxDisplaySettings.X;
            form.Top = pbxDisplaySettings.Y;
            form.Width = pbxDisplaySettings.Width;
            form.Height = pbxDisplaySettings.Height;

            if (!string.IsNullOrEmpty(pbxDisplaySettings.ImagePath))
            {
                var matches = Directory.GetFiles(pbxDisplaySettings.ImagePath, $"{tablename}.*");
                if (matches.Any())
                {
                    string imageFileName = Path.Combine(pbxDisplaySettings.ImagePath, matches.First());
                    var image = Image.FromFile(imageFileName);
                    if (pbxDisplaySettings.Rotate != 0)
                    {
                        RotateImage(ref image, pbxDisplaySettings.Rotate);
                    }
                    form.BackgroundImage= image;
                    //background.Dock = DockStyle.Fill;
                }
            }
        }

        private Image Resize(Image image, int width, int height   ) 
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }

        public void DisplaySelection(Form form, DisplaySettings displaySettings, int currentNumberOfPlayers = 1)
        {
            if (!displaySettings.ShowImage)
            {
                _logger.Log(LogLevel.Debug, $"No selection on this display for this table");
                return;
            }

            var player = form.Controls.OfType<PictureBox>().FirstOrDefault(q => q.Tag.Equals("Player"));
            if (player == null) throw new Exception("PictureBox not found");

            string pattern = $"{displaySettings.Prefix}{currentNumberOfPlayers}.*";
            var fileMatches = Directory.GetFiles(".\\pix", pattern);
            if (!fileMatches.Any())
            {
                _logger.Warn($"Cannot find any file with pattern '{pattern}'");
                return;
            }

            var selectionImage = Image.FromFile(fileMatches.First());
            var resizedImage= Resize(selectionImage, displaySettings.Width * form.Width / 100, displaySettings.Height * form.Height / 100);
            if (displaySettings.Rotate != 0)
            {
                RotateImage(ref resizedImage, displaySettings.Rotate);
            }

            player.Image = resizedImage;
            player.Width = resizedImage.Width;
            player.Height = resizedImage.Height;


            player.Left = (form.Width - player.Width) / 2;
            player.Top = (form.Height - player.Height) / 2;
            player.BackColor = Color.Transparent;
        }
    }
}
