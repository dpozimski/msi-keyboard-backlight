using MSI.Keyboard.Backlight.Enums;

namespace MSI.Keyboard.Backlight.Configuration
{
    public class RegionBacklightConfiguration
    {
        public System.Drawing.Color Color { get; }
        public int Intensity { get; }

        public RegionBacklightConfiguration(System.Drawing.Color color, int intensity)
        {
            Color = color;
            Intensity = intensity;
        }
    }
}