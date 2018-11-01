using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Device;
using MSI.Keyboard.Backlight.Utils;

namespace MSI.Keyboard.Backlight.Service
{
    public static class KeyboardServiceFactory
    {
        public static IKeyboardService Create()
        {
            var colorEnumToRgbConverter = new ColorEnumToRgbConverter();
            var keyboardDevice = new HIDKeyboardDevice(colorEnumToRgbConverter);
            return new KeyboardService(keyboardDevice);
        }
    }
}
