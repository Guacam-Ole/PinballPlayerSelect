using IniParser;
using IniParser.Model;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPS
{
    public partial class Import : Form
    {
        private const string _PinballFXSteamId = "2328760";
        private const string _PinballMSteamId = "2337640";
        private const string _tagOverlay = "Overlay";
        private const string _tagScreen = "Screen";
        private const string _fx3Section = "PinballFX3";
        private const string _separator = "|";
        private KeyDataCollection _pinballFx = null;
        private KeyDataCollection _pinballM = null;
        private KeyDataCollection _pinballFx3 = null;
        private ConfigValues _configValues;
        private readonly Timer _previewTimer = new() { Interval = 2400 };
        private readonly Random _random = new();
        private readonly ILogger<Import> _logger;
        private IniData _iniData;

        public Import(ILogger<Import> logger) : this()
        {
            _logger = logger;
        }

        public Import()
        {
            InitializeComponent();
            FillComboValues();
            _previewTimer.Tick += _previewTimer_Tick;
        }

        private void _previewTimer_Tick(object sender, EventArgs e)
        {
            int numPlayers = _random.Next(4) + 1;
            foreach (var group in GetGroupsByParentTag(_tagOverlay))
            {
                var combo = group.Controls.OfType<ComboBox>().First(q => q.Tag.Equals("OverlayStyle"));
                ShowImageFromCombo(combo, numPlayers);
            }
        }

        private void pbxBrowse_Click(object sender, EventArgs e)
        {
            VerifyExePath.Enabled = false;

            var pbxDialog = new OpenFileDialog { CheckFileExists = !Debugger.IsAttached, CheckPathExists = true, Filter = "PinballX Executable|pinballX.exe" };
            var selectedPbx = pbxDialog.ShowDialog();

            if (selectedPbx == DialogResult.OK)
            {
                VerifyExePath.Enabled = true;
                pbxInput.Text = pbxDialog.FileName;
            }
        }

        private string GetIniFilePath()
        {
            string pbxPath = Path.GetDirectoryName(pbxInput.Text);
            return Path.Combine(pbxPath, "Config");
        }

        private void VerifyExePath_Click(object sender, EventArgs e)
        {
            try
            {
                ParseIni(Path.Combine(GetIniFilePath(), "PinballX.ini"));
                _logger.LogInformation("Retrieved Ini-File");
                Screen.Enabled = true;
                Overlays.Enabled = true;
                WriteConfig.Enabled = true;
                SetDefaultOverlay();

                _previewTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sorry, your Silverball got stuck somehow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.LogError(ex, "Cannot read ini");
            }
        }

        private static Screen GetScreenFromIni(KeyDataCollection data, string prefix = null)
        {
            int id = data.IntParse("Monitor");
            if (id + 1 > System.Windows.Forms.Screen.AllScreens.Length)
            {
                MessageBox.Show($"ScreenId '{id}' does not exist");
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

        private static bool IsFullScreen(int screenId, int width, int height)
        {
            var screen = System.Windows.Forms.Screen.AllScreens[screenId];
            return screen.WorkingArea.Width == width && screen.WorkingArea.Height == height;
        }

        private void AddToEmulatorIfConfigExists(List<Emulator> emulators, string emulatorName, KeyDataCollection iniContents)
        {
            if (iniContents == null) return;
            if (iniContents["Executable"] == "PPS.exe")
            {
                MessageBox.Show("Ini has already been modified (I found pps.exe there)");
                this.Close();
                return;
            }
            emulators.Add(new Emulator
            {
                Name = emulatorName,
                SectionName = iniContents["Section"],
                Executable = iniContents["Executable"],
                WorkingPath = iniContents["WorkingPath"],
                Media = Path.GetDirectoryName(pbxInput.Text)
            });
        }

        private void ParseIni(string filename)
        {
            var parser = new FileIniDataParser();
            _iniData = parser.ReadFile(filename);

            if (_iniData.Sections.Any(q => q.SectionName == _fx3Section))
            {
                _pinballFx3 = _iniData[_fx3Section];
                _pinballFx3["Section"] = _fx3Section;
            }

            var input = _iniData["KeyCodes"];

            for (int sectionCount = 1; sectionCount < 10; sectionCount++)
            {
                string sectionName = $"System_{sectionCount}";
                var systemSection = _iniData.Sections.FirstOrDefault(q => q.SectionName == sectionName);
                if (systemSection == null) continue;
                if (systemSection.Keys.ContainsKey("Parameters"))
                {
                    var parameters = _iniData[sectionName]["Parameters"];
                    if (parameters.Contains(_PinballFXSteamId))
                    {
                        _pinballFx = _iniData[sectionName];
                        _pinballFx["Section"] = sectionName;
                    }
                    if (parameters.Contains(_PinballMSteamId))
                    {
                        _pinballM = _iniData[sectionName];
                        _pinballM["Section"] = sectionName;
                    }
                }
            }

            var emulators = new List<Emulator>();
            AddToEmulatorIfConfigExists(emulators, "PinballFX3", _pinballFx3);
            AddToEmulatorIfConfigExists(emulators, "PinballFX", _pinballFx);
            AddToEmulatorIfConfigExists(emulators, "PinballM", _pinballM);

            _configValues = new ConfigValues()
            {
                BatchMode = false,
                StayOpen = false,
                Emulators = emulators,
                Input = new Input
                {
                    Exit = input.IntParse("quit"),
                    MorePlayers = input.IntParse("right"),
                    LessPlayers = input.IntParse("left"),
                    StartGame = input.IntParse("select"),
                    Loop = true,
                    PlayerCountAtStart = 1
                },
            };

            SetScreenPropertiesFromIni(_iniData, "DMD");
        }

        private void SetDefaultOverlay()
        {
            var dmd = GetOverlayGroup("DMD");
            var combo = dmd.Controls.OfType<ComboBox>().First(q => q.Tag.Equals("OverlayStyle"));
            var items = (List<KeyValuePair<string, string>>)combo.DataSource;
            combo.SelectedValue = items.First(q => q.Value == "indyhands").Key;
        }

        private static Dictionary<string, Control> GetGroupControls(GroupBox parent, params string[] controlNames)
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

        private static KeyValuePair<string, Control> GetSingleControlByTag(GroupBox parent, string tag)
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
            return GetSubGroup(_tagScreen, screen);
        }

        private GroupBox GetOverlayGroup(string overlay)
        {
            return GetSubGroup(_tagOverlay, overlay);
        }

        private GroupBox GetSubGroup(string parentTag, string subTag)
        {
            return GetGroupsByParentTag(parentTag).First(q => q.Tag.Equals(subTag));
        }

        private void FillComboValues()
        {
            var screen = GetGroupsByParentTag(_tagScreen).First();
            var overlay = GetGroupsByParentTag(_tagOverlay).First();

            SetDataSource(screen, "ScreenId", GetValidScreens());
            SetDataSource(overlay, "OverlayStyle", GetValidOverlays());
        }

        private static void SetDataSource<T>(GroupBox groupBox, string tag, Dictionary<T, string> dataSource)
        {
            var combo = groupBox.Controls.OfType<ComboBox>().First(q => q.Tag.Equals(tag));
            combo.Items.Clear();
            combo.DataSource = dataSource.ToList();
        }

        private static Dictionary<int, string> GetValidScreens()
        {
            var screens = new Dictionary<int, string>();
            int counter = 0;
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                screens.Add(counter++, screen.DeviceName);
            }
            return screens;
        }

        private static Dictionary<string, string> GetValidOverlays()
        {
            var filenames = new Dictionary<string, string>() { { "", "(none)" } };
            var overlays = Directory.GetFiles(".\\pix", "*1.*");
            foreach (var overlay in overlays)
            {
                string filename = Path.GetFileName(overlay);
                filenames.Add(overlay, filename[..(filename.LastIndexOf('.') - 1)]);
            }
            return filenames;
        }

        private void ScreenCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            string oldTitle = combo.Parent.Text;
            string prefix = oldTitle.Contains('|')
                ? oldTitle[..oldTitle.IndexOf(_separator)]
                : oldTitle;
            combo.Parent.Text = prefix + "|" + GetValidScreens()[(int)combo.SelectedValue];
        }

        private void BackGlassOverlay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageFromCombo((ComboBox)sender);
        }

        private static void ShowImageFromCombo(ComboBox combo, int playerNumber = 1)
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
                if (MessageBox.Show("Warning! Your configfile (config.json) and the PinballX.Ini will be overwritten. Make sure you have a backup. Continue?", "Writing Config", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }

                FinishConfig();
                SaveConfig();
                SaveIni();

                MessageBox.Show("Everything should work now. A backup has been created");
                _logger.LogInformation("Created Configs");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sorry, your Silverball got stuck somehow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.LogError(ex, "Error writing config");
            }
        }

        private void FinishConfig()
        {
            _configValues.Dmd = GetScreenConfigFromUi("DMD");
            _configValues.Overlays = [GetOverlayConfigFromUi("DMD")];
        }

        private void SaveConfig()
        {
            if (File.Exists("config.json")) File.Copy("config.json", $"config.backup-{DateTime.Now.ToFileTime()}.json");
            var configContents = JsonConvert.SerializeObject(_configValues, Formatting.Indented);
            File.WriteAllText("config.json", configContents);
        }

        private void SaveIni()
        {
            var iniFilePath = Path.Combine(GetIniFilePath(), "PinballX.ini");
            File.Copy(iniFilePath, Path.Combine(GetIniFilePath(), $"PinballX.backup-{DateTime.Now.ToFileTime()}.ini"));
            foreach (var emulator in _configValues.Emulators)
            {
                var emulatorIni = _iniData[emulator.SectionName];
                emulatorIni["WorkingPath"] = AppDomain.CurrentDomain.BaseDirectory;
                emulatorIni["Executable"] = "PPS.exe";
                emulatorIni["Parameters"] = $"{emulator.Name} [TABLEFILE] {emulatorIni["Parameters"]}";
            }
            var streamIniParser = new StreamIniDataParser();
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                streamIniParser.WriteData(streamWriter, _iniData);
            }
            var fixedIniContents = Encoding.ASCII.GetString(memoryStream.ToArray());
            fixedIniContents = fixedIniContents.Replace(" = ", "=");
            File.WriteAllText(iniFilePath, fixedIniContents);
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

        private static void TrackbarToLabel(object sender, string labelTag)
        {
            var trackbar = (TrackBar)sender;
            var parent = trackbar.Parent;
            var label = parent.Controls.OfType<Label>().First(q => q.Tag != null && q.Tag.Equals(labelTag));
            label.Text = trackbar.Value + " %";
        }
    }
}