using MSI.Keyboard.Backlight.Enums;

namespace MSI.Keyboard.Backlight.Configuration
{
    public class RegionBacklightConfiguration
    {
        public Color Color { get; }
        public Intensity Intensity { get; }

        public RegionBacklightConfiguration(Color color, Intensity intensity)
        {
            Color = color;
            Intensity = intensity;
        }
    }
}