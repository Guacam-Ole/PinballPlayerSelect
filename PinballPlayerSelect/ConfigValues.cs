using System.Collections.Generic;

namespace PPS
{
    public class ConfigValues
    {
        public Screen Dmd { get; set; }
        public Input Input { get; set; }
        public List<Emulator> Emulators { get; set; }
        public List<Overlay> Overlays { get; set; }
        public bool StayOpen { get; set; } = false;
        public bool BatchMode { get; set; } = false;
    }

    public class Input
    {
        public int Exit { get; set; }
        public int StartGame { get; set; }
        public int MorePlayers { get; set; }
        public int LessPlayers { get; set; }
        public bool Loop { get; set; } = true;
        public int OnePlayer { get; set; }
        public int TwoPlayers { get; set; }
        public int ThreePlayers { get; set; }
        public int FourPlayers { get; set; }
        public int PlayerCountAtStart { get; set; } = 1;
    }

    public class Emulator
    {
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string WorkingPath { get; set; }
        public string Executable { get; set; }
        public string OnePlayer { get; set; }
        public string TwoPlayers { get; set; }
        public string ThreePlayers { get; set; }
        public string FourPlayers { get; set; }
        public string Media { get; set; }
    }

    public class Overlay
    {
        public string? Filter { get; set; } = null;
        public string Prefix { get; set; }
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 100;
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
        public bool Background { get; set; } = true;
        public bool OnTop { get; set; } = false;
    }
}