using System.Drawing;

namespace MSI.Keyboard.Backlight.Utils
{
    public interface IColorEnumToRgbConverter
    {
        Color ToRgb(Enums.Color colorEnum);
    }
}
