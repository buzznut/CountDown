using System;
using System.Drawing;

namespace Utilities
{
    public class Settings
    {
        public string DisplayText { get; set; }
        public string FinishText { get; set; }
        public DateTime EndDate { get; set; }
        public string Monitor { get; set; }
        public string Format { get; set; }
        public Font Font { get; set; }
        public Color Color { get; set; }
        public ScreenPosition ScreenPosition { get; set; }
        public Point? Override { get; set; }
    }
}

