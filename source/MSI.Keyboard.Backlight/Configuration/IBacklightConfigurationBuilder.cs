using MSI.Keyboard.Backlight.Enums;

namespace MSI.Keyboard.Backlight.Configuration
{
    public interface IBacklightConfigurationBuilder
    {
        IBacklightConfigurationBuilder ForAllRegions(BlinkingMode blinkingMode);
        IBacklightConfigurationBuilder ForAllRegions(Color color, Intensity intensity);
        IBacklightConfigurationBuilder ForAllRegions(System.Drawing.Color color, int intensity);
        IBacklightConfigurationBuilder ForRegion(Region region, Color color, Intensity intensity);
        IBacklightConfigurationBuilder ForRegion(Region region, System.Drawing.Color color, int intensity);

        BacklightConfiguration Build();
    }
}
