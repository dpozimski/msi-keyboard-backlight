using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Enums;

namespace MSI.Keyboard.Backlight.Utils
{
    internal class ColorEnumToRgbConverter : IColorEnumToRgbConverter
    {
        public System.Drawing.Color ToRgb(Enums.Color colorEnum)
        {
            switch(colorEnum)
            {
                case Enums.Color.Blue:
                    return System.Drawing.Color.Blue;
                case Enums.Color.Green:
                    return System.Drawing.Color.Green;
                case Enums.Color.Off:
                    return System.Drawing.Color.Black;
                case Enums.Color.Orange:
                    return System.Drawing.Color.Orange;
                case Enums.Color.Purple:
                    return System.Drawing.Color.Purple;
                case Enums.Color.Red:
                    return System.Drawing.Color.Red;
                case Enums.Color.Sky:
                    return System.Drawing.Color.LightBlue;
                case Enums.Color.White:
                    return System.Drawing.Color.White;
                case Enums.Color.Yellow:
                    return System.Drawing.Color.Yellow;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
