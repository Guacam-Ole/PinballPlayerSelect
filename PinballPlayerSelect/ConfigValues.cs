using System.Collections.Generic;

namespace PinballPlayerSelect
{
    public class ConfigValues
    {
        public Media Media { get; set; }
        public Screens Screens { get; set; }
        public Input Input { get; set; }
        public Launch Launch { get; set; }
        public List<OverlayGroup> Overlays { get; set; }
        public bool StayOpen { get; set; } = false;
    }

    public class OverlayGroup
    {
        public string Filter { get; set; }
        public Overlay BackGlass { get; set; }
        public Overlay Dmd { get; set; }
        public Overlay PlayField { get; set; }
    }

    public class Input
    {
        public int Exit { get; set; }
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

    public class Overlay
    {

        public string Prefix { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        
    }

    public class Media
    {
        public string Root { get; set; }
        public string PlayField { get; set; }
        public string BackGlass { get; set; }
        public string Dmd { get; set; }
    }

    public class Screens
    {
        public Screen PlayField { get; set; } = new Screen() { Enabled = false };
        public Screen BackGlass { get; set; } = new Screen() { Enabled = false };
        public Screen Dmd { get; set; } = new Screen() { Enabled = false };
    }

    public class Screen
    {
        public int Id { get; set; }
        public int Rotate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int OverlayRotate { get; set; }
        public bool Enabled { get; set; } = true;
        public bool Background { get; set; } = true;
    }
}