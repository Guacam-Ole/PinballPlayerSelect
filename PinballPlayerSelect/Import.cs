using IniParser.Model;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PPS
{
    public partial class Import : Form
    {
        private ConfigValues _configValues;
        private readonly Timer _previewTimer = new() { Interval = 2400 };
        private readonly Random _random = new Random();

        public Import()
        {
            InitializeComponent();
            FillComboValues();
            _previewTimer.Tick += _previewTimer_Tick;
        }

        private void _previewTimer_Tick(object sender, EventArgs e)
        {
            int numPlayers = _random.Next(4) + 1;
            foreach (var group in GetGroupsByParentTag("Overlays"))
            {
                var combo = group.Controls.OfType<ComboBox>().First(q => q.Tag.Equals("OverlayStyle"));
                ShowImageFromCombo(combo, numPlayers);
            }
        }


        private void pbxBrowse_Click(object sender, EventArgs e)
        {
            VerifyExePath.Enabled = false;
            var pbxDialog = new OpenFileDialog { CheckFileExists = true, CheckPathExists = true, Filter = "PinballX Executable|pinballX.exe" };
            var selectedPbx = pbxDialog.ShowDialog();

            if (selectedPbx == DialogResult.OK)
            {
                VerifyExePath.Enabled = true;
                pbxInput.Text = pbxDialog.FileName;
            }
        }

        private void VerifyExePath_Click(object sender, EventArgs e)
        {
            try
            {
                string pbxPath = Path.GetDirectoryName(pbxInput.Text);
                ParseIni(Path.Combine(pbxPath, "Config", "PinballX.ini"));
                SetPaths(pbxPath);
                Screens.Enabled = true;
                Overlays.Enabled = true;
                Subs.Enabled = true;
                WriteConfig.Enabled = true;
                SetDefaultOverlay();

                _previewTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Sorry, your silverball got stuck somehow", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetPaths(string rootpath)
        {
            PathBackglass.Text = Path.Combine(rootpath, "Media", "Pinball FX3", "Backglass Images");
            PathDmd.Text = Path.Combine(rootpath, "Media", "Pinball FX3", "DMD Images");
            PathPlayfield.Text = Path.Combine(rootpath, "Media", "Pinball FX3", "Table Images");
        }

        private Screen GetScreenFromIni(KeyDataCollection data, string prefix = null)
        {
            int id = data.IntParse("Monitor");
            if (id + 1 > System.Windows.Forms.Screen.AllScreens.Length)
            {
                MessageBox.Show($"Screenid {id} does not exist");
                id = System.Windows.Forms.Screen.AllScreens.Length - 1;
            }

            int x = data.IntParse("X", prefix);
            int y = data.IntParse("Y", prefix);
            int width = data.IntParse("Width", prefix);
            int height = data.IntParse("Height", prefix);
            int rotate = data.IntParse("Rotate");

            var screen = new Screen
            {
                Id = id,
                Rotate = rotate,
                X = x,
                Y = y
            };

            if (!IsFullScreen(id, width, height))
            {
                screen.Width = width;
                screen.Height = height;
            }
            return screen;
        }

        private bool IsFullScreen(int screenId, int width, int height)
        {
            var screen = System.Windows.Forms.Screen.AllScreens[screenId];
            return screen.WorkingArea.Width == width && screen.WorkingArea.Height == height;
        }

        private void ParseIni(string filename)
        {
            var parser = new IniParser.Parser.IniDataParser();
            var iniData = parser.Parse(File.ReadAllText(filename));
            var fx3 = iniData["PinballFX3"];
            var input = iniData["KeyCodes"];

            _configValues = new ConfigValues()
            {
                BatchMode = false,
                StayOpen = false,
                Launch = new Launch
                {
                    Executable = fx3["Executable"],
                    Parameters = fx3["Parameters"] + " [PLAYER]",
                    WorkingPath = fx3["WorkingPath"]
                },
                Input = new Input
                {
                    Exit = input.IntParse("quit"),
                    MorePlayers = input.IntParse("right"),
                    LessPlayers = input.IntParse("left"),
                    StartGame = input.IntParse("select"),
                    Loop = true,
                    PlayerCountAtStart = 1
                },
                Media = new Media
                {
                    Root = Path.GetDirectoryName(pbxInput.Text)
                }
            };

            SetScreenPropertiesFromIni(iniData, "Display", "Window");
            SetScreenPropertiesFromIni(iniData, "DMD");
            SetScreenPropertiesFromIni(iniData, "BackGlass");
        }

        private void SetDefaultOverlay()
        {
            var dmd = GetOverlayGroup("DMD");
            var combo = dmd.Controls.OfType<ComboBox>().First(q => q.Tag.Equals("OverlayStyle"));
            var items = (List<KeyValuePair<string, string>>)combo.DataSource;
            combo.SelectedValue = items.First(q => q.Value == "indyhands").Key;
        }

        private Dictionary<string, Control> GetGroupControls(GroupBox parent, params string[] controlNames)
        {
            var uiControls = new List<KeyValuePair<string, Control>>();
            foreach (var controlname in controlNames)
            {
                uiControls.Add(GetSingleControlByTag(parent, controlname));
            }

            return uiControls.ToDictionary(q => q.Key, q => q.Value);
        }

        private Dictionary<string, Control> GetScreenControls(string screenName)
        {
            return GetGroupControls(GetScreenGroup(screenName), "ScreenId", "ScreenX", "ScreenY", "ScreenWidth", "ScreenHeight", "ScreenRotate", "OverlayRotate");
        }

        private Dictionary<string, Control> GetOverlayControls(string screenName)
        {
            return GetGroupControls(GetOverlayGroup(screenName), "OverlayStyle", "OverlayWidth", "OverlayHeight");
        }

        private KeyValuePair<string, Control> GetSingleControlByTag(GroupBox parent, string tag)
        {
            return new KeyValuePair<string, Control>(tag, parent.Controls.OfType<Control>().First(q => q.Tag != null && q.Tag.Equals(tag)));
        }

        private void SetScreenPropertiesFromIni(IniData data, string screenName, string prefix = null)
        {
            var controls = GetScreenControls(screenName);
            var screenData = GetScreenFromIni(data[screenName], prefix);

            ((ComboBox)controls["ScreenId"]).SelectedValue = screenData.Id;
            controls["ScreenX"].Text = screenData.X.ToString();
            controls["ScreenY"].Text = screenData.Y.ToString();
            controls["ScreenWidth"].Text = screenData.Width.ToString();
            controls["ScreenHeight"].Text = screenData.Height.ToString();
            controls["ScreenRotate"].Text = screenData.Rotate.ToString();
            controls["OverlayRotate"].Text = screenName.Equals("Display", StringComparison.CurrentCultureIgnoreCase) ? (screenData.Rotate - 90).ToString() : screenData.Rotate.ToString();
        }

        private IEnumerable<GroupBox> GetGroupsByParentTag(string tag)
        {
            var groupBoxes = Controls.OfType<GroupBox>();
            return groupBoxes.First(q => q.Tag.Equals(tag)).Controls.OfType<GroupBox>();
        }

        private GroupBox GetScreenGroup(string screen)
        {
            return GetSubGroup("Screens", screen);
        }

        private GroupBox GetOverlayGroup(string overlay)
        {
            return GetSubGroup("Overlays", overlay);
        }

        private GroupBox GetSubGroup(string parentTag, string subTag)
        {
            return GetGroupsByParentTag(parentTag).First(q => q.Tag.Equals(subTag));
        }

        private void FillComboValues()
        {
            var screens = GetGroupsByParentTag("Screens");
            var overlays = GetGroupsByParentTag("Overlays");

            foreach (var screen in screens)
            {
                var combo = screen.Controls.OfType<ComboBox>().First(q => q.Tag.Equals("ScreenId"));
                combo.Items.Clear();
                combo.DataSource = GetValidScreens().ToList();
            }
            foreach (var overlay in overlays)
            {
                var combo = overlay.Controls.OfType<ComboBox>().First(q => q.Tag.Equals("OverlayStyle"));
                combo.Items.Clear();
                combo.DataSource = GetValidOverlays().ToList();
            }
        }

        private Dictionary<int, string> GetValidScreens()
        {
            var screens = new Dictionary<int, string>();
            int counter = 0;
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                screens.Add(counter++, screen.DeviceName);
            }
            return screens;
        }

        private Dictionary<string, string> GetValidOverlays()
        {
            var filenames = new Dictionary<string, string>() { { "", "(none)" } };
            var overlays = Directory.GetFiles(".\\pix", "*1.*");
            foreach (var overlay in overlays)
            {
                string filename = Path.GetFileName(overlay);
                filenames.Add(overlay, filename.Substring(0, filename.LastIndexOf('.') - 1));
            }
            return filenames;
        }

        private void ScreenCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            string oldTitle = combo.Parent.Text;
            string prefix = oldTitle.Contains("|")
                ? oldTitle.Substring(0, oldTitle.IndexOf("|"))
                : oldTitle;
            combo.Parent.Text = prefix + "|" + GetValidScreens()[(int)combo.SelectedValue];
        }

        private void BackGlassOverlay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageFromCombo((ComboBox)sender);
        }

        private void ShowImageFromCombo(ComboBox combo, int playerNumber = 1)
        {
            var pictureBox = combo.Parent.Controls.OfType<PictureBox>().First();
            var filename = (string)combo.SelectedValue;
            var lastOne = filename.LastIndexOf('1');
            if (lastOne > 0)
            {
                filename = $"{filename[..lastOne]}{playerNumber}{filename[(lastOne + 1)..]}";
            }
            pictureBox.Image = filename == String.Empty ? null : Image.FromFile(filename);
        }

        private void WriteConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Warning! Your configfile (config.json) will be overwritten. Make sure you have a backup. Continue?", "Writing Config", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }

                FinishConfig();
                SaveConfig();

                new DisplayText(
                    "Copy this stuff!",
                    "One final step: You have to modify a few lines in PinballX.INI. Because we don't never ever want to " +
                    "mess up your pinballX-Configuration we don't do this automatically. " +
                    "Please copy and paste the following lines yourself to the \"[PinballFX3]\" segment into the PinbalX.ini:",
                    
                   $"WorkingPath={AppDomain.CurrentDomain.BaseDirectory}\r\n" +
                    "Executable = PPS.exe\r\n" +
                    "Parameters =[TABLEFILE]\r\n"
                    ).ShowDialog();

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Sorry, your silverball got stuck somehow", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FinishConfig()
        {
            _configValues.Screens = new Screens
            {
                BackGlass = GetScreenConfigFromUi("BackGlass"),
                Dmd = GetScreenConfigFromUi("DMD"),
                PlayField = GetScreenConfigFromUi("Display")
            };

            _configValues.Overlays = new List<OverlayGroup>
            {
                new OverlayGroup
                {
                     PlayField=GetOverlayConfigFromUi("Display"),
                      BackGlass=GetOverlayConfigFromUi("BackGlass"),
                       Dmd=GetOverlayConfigFromUi("DMD")
                }
            };
        }

        private void SaveConfig()
        {
            var configContents = JsonConvert.SerializeObject(_configValues);
            File.WriteAllText(Path.Combine(".\\pix", "config.json"), configContents);
        }

        private Overlay GetOverlayConfigFromUi(string tag)
        {
            var controls = GetOverlayControls(tag);
            var prefix = ((KeyValuePair<string, string>)((ComboBox)controls["OverlayStyle"]).SelectedItem);
            if (string.IsNullOrEmpty(prefix.Key)) return null;
            return new Overlay
            {
                Prefix = prefix.Value,
                Width = ((TrackBar)controls["OverlayWidth"]).Value,
                Height = ((TrackBar)controls["OverlayHeight"]).Value,
            };
        }

        private Screen GetScreenConfigFromUi(string tag)
        {
            var controls = GetScreenControls(tag);

            return new Screen
            {
                Background = true,
                Enabled = true,
                Id = (int)((ComboBox)controls["ScreenId"]).SelectedValue,

                X = int.Parse(controls["ScreenX"].Text),
                Y = int.Parse(controls["ScreenY"].Text),
                Width = int.Parse(controls["ScreenWidth"].Text),
                Height = int.Parse(controls["ScreenHeight"].Text),
                Rotate = int.Parse(controls["ScreenRotate"].Text),
                OverlayRotate = int.Parse(controls["OverlayRotate"].Text),
                OnTop = false,
            };
        }

        private void OverlayWidth_Scroll(object sender, EventArgs e)
        {
            TrackbarToLabel(sender, "WidthLabel");
        }

        private void OverlayHeight_Scroll(object sender, EventArgs e)
        {
            TrackbarToLabel(sender, "HeightLabel");
        }

        private void TrackbarToLabel(object sender, string labelTag)
        {
            var trackbar = (TrackBar)sender;
            var parent = trackbar.Parent;
            var label = parent.Controls.OfType<Label>().First(q => q.Tag != null && q.Tag.Equals(labelTag));
            label.Text = trackbar.Value + " %";
        }
    }
}