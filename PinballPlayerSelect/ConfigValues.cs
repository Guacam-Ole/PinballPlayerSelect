using System;
using System.Collections.Generic;
using System.Text;

namespace PinballPlayerSelect
{
    public class ConfigValues
    {
        public PinballX PinballX { get; set; }
        public DisplaySettings BackGlass { get; set; }
        public DisplaySettings Dmd { get; set; }
        public DisplaySettings PlayField { get; set; }
        public Select Selection { get; set; }
        public Launch Launch { get; set; }
    }

    public class Select
    {
        public char ReturnToPinballX { get; set; }
        public char StartGame { get; set; }
        public char MorePlayers { get; set; }
        public char LessPlayers { get; set; }
        public bool Loop { get; set; } = true;
        public char OnePlayer { get; set; }
        public char TwoPlayers { get; set; }
        public char ThreePlayers { get; set; }
        public char FourPlayers { get; set; }
        public int PlayerCountAtStart { get; set; }
    }

    public class Launch
    {
        public string Path { get; set; }
        public string Parameters { get; set; }
        public string OnePlayer { get; set; }
        public string TwoPlayers { get; set; }
        public string ThreePlayers { get; set; }
        public string FourPlayers { get; set; }
    }

    public class DisplaySettings
    {
        public bool ShowVideo { get; set; }
        public bool ShowImage { get; set; }
        public bool Display { get; set; }
        public string Prefix { get; set; }
        public int Width { get; set; }  
        public int Height { get; set; }
    }

    public class PinballX
    {
        public Media Media{ get; set; }
        public string ConfigIniFile { get; set; }
        public PinballXDisplay Table { get; set; }
        public PinballXDisplay BackGlass { get; set; }
        public PinballXDisplay Dmd { get; set; }
    }

    public class Media
    {
        public string Path { get; set; }    
        public MediaPath BackGlass { get; set; }    =new MediaPath();
        public MediaPath Table { get; set; }=new MediaPath();
        public MediaPath Dmd { get; set; } = new MediaPath();
    }

    public class MediaPath
    {
        public string Videos { get; set; }  
        public string Images { get; set; }  
    }

    public class PinballXDisplay
    {
        public int Monitor { get; set; }
        public int Rotate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }


}
