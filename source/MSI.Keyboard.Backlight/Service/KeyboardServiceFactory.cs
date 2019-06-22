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
