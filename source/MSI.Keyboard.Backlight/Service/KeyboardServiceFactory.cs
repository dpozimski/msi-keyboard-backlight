using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Device;

namespace MSI.Keyboard.Backlight.Service
{
    public static class KeyboardServiceFactory
    {
        public static IKeyboardService Create()
        {
            var keyboardDevice = new HIDKeyboardDevice();
            return new KeyboardService(keyboardDevice);
        }
    }
}
