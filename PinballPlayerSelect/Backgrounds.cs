using NLog;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PPS
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

        public Backgrounds(ILogger logger)
        {
            _logger = logger;
        }

        public Backgrounds()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        private static void RotateImage(ref Image image, int degrees)
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

        public static void PaintBackgroundImage(Form form, Screen screen, string imagePath, string tablename)
        {
            if (System.Windows.Forms.Screen.AllScreens.Length < screen.Id + 1)
            {
                OutputHelper.ShowMessage($"Screen {screen.Id} does not exist");
                return;
            }
            var winScreen = System.Windows.Forms.Screen.AllScreens[screen.Id];

            form.Left = screen.X + winScreen.WorkingArea.Location.X;
            form.Top = screen.Y + winScreen.WorkingArea.Location.Y;
            if (screen.Width > 0 && screen.Height > 0)
            {
                form.Width = screen.Width;
                form.Height = screen.Height;
            }
            else
            {
                form.Width = winScreen.WorkingArea.Width;
                form.Height = winScreen.WorkingArea.Height;
            }

            if (!string.IsNullOrEmpty(imagePath))
            {
                if (!Directory.Exists(imagePath))
                {
                    OutputHelper.ShowMessage($"Cannot open path {imagePath}");
                    return;
                }

                var matches = Directory.GetFiles(imagePath, $"{tablename}.*");
                string imageFileName;
                if (matches.Length != 0)
                {
                    imageFileName = Path.Combine(imagePath, matches.First());
                }
                else
                {
                    imageFileName = $"pix\\missing_{form.Tag}.png";
                }

                var image = Image.FromFile(imageFileName);
                if (screen.Rotate != 0)
                {
                    RotateImage(ref image, screen.Rotate);
                }
                form.BackgroundImage = image;
            }
        }

        private static Image Resize(Image image, int width, int height)
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

        public void DisplaySelection(Form form, Screen screensettings, Overlay overlaySettings, int currentNumberOfPlayers = 1)
        {
            if (overlaySettings?.Prefix == null)
            {
                _logger.Log(LogLevel.Debug, $"No selection on this display for this table");
                return;
            }

            var player = form.Controls.OfType<PictureBox>().FirstOrDefault(q => q.Tag.Equals("Player")) ?? throw new Exception("PictureBox not found");
            string pattern = $"{overlaySettings.Prefix}{currentNumberOfPlayers}.*";
            var fileMatches = Directory.GetFiles(".\\pix", pattern);
            if (fileMatches.Length == 0)
            {
                OutputHelper.ShowMessage($"Cannot find any file with pattern '{pattern}'");
                return;
            }

            var selectionImage = Image.FromFile(fileMatches.First());
            var resizedImage = Resize(selectionImage, overlaySettings.Width * form.Width / 100, overlaySettings.Height * form.Height / 100);
            if (screensettings.OverlayRotate != 0)
            {
                RotateImage(ref resizedImage, screensettings.OverlayRotate);
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