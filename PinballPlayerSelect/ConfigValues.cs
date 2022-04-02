using System;
using System.Collections.Generic;
using System.Text;

namespace PinballPlayerSelect
{
    public class ConfigValues
    {
        public PinballX PinballX { get; set; }
        public Select Selection { get; set; }
        public Launch Launch { get; set; }
        public List<DisplayGroup> Displays { get; set; }
    }

    public class DisplayGroup
    {
        public string File { get; set; }
        public DisplaySettings BackGlass { get; set; }
        public DisplaySettings Dmd { get; set; }
        public DisplaySettings Table { get; set; }
    }

    public class Select
    {
        public int ReturnToPinballX { get; set; }
        public int StartGame { get; set; }
        public int MorePlayers { get; set; }
        public int LessPlayers { get; set; }
        public bool Loop { get; set; } = true;
        public int OnePlayer { get; set; }  // Directly start as 1 Player Game
        public int TwoPlayers { get; set; } // Directly start as 2 Player Game
        public int ThreePlayers { get; set; }
        public int FourPlayers { get; set; }
        public int PlayerCountAtStart { get; set; } = 1;
    }

    public class Launch
    {
        public string WorkingPath { get; set; }
        public string Executable { get; set; }
        public string Parameters { get; set; }
        public string OnePlayer { get; set; }
        public string TwoPlayers { get; set; }
        public string ThreePlayers { get; set; }
        public string FourPlayers { get; set; }
    }

    public class DisplaySettings
    {
        public bool Enabled { get; set; } = true;
        public bool ShowImage { get; set; }
        public bool Background { get; set; }
        public string Prefix { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Rotate { get; set; } = -1;
    }

    public class PinballX
    {
        public string MediaPath { get; set; }
        public string ConfigIniFile { get; set; }
        public PinballXDisplay Table { get; set; }
        public PinballXDisplay BackGlass { get; set; }
        public PinballXDisplay Dmd { get; set; }
    }

    //public class Media
    //{
    //    public string Path { get; set; }
    //    public MediaPath BackGlass { get; set; } = new MediaPath();
    //    public MediaPath Table { get; set; } = new MediaPath();
    //    public MediaPath Dmd { get; set; } = new MediaPath();
    //}

    //public class MediaPath
    //{
    //    public string Videos { get; set; }
    //    public string Images { get; set; }
    //}

    public class PinballXDisplay
    {
        public int Monitor { get; set; }
        public int Rotate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string ImagePath { get; set; }
    }


}
