using MSI.Keyboard.Backlight.Utils;

namespace MSI.Keyboard.Backlight.Configuration
{
    public static class BacklightConfigurationBuilderFactory
    {
        public static IBacklightConfigurationBuilder Create()
        {
            var colorEnumToRgbConverter = new ColorEnumToRgbConverter();
            return new BacklightConfigurationBuilder(colorEnumToRgbConverter);
        }
    }
}
