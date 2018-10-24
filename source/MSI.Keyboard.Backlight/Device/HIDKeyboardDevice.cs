using HidLibrary;
using MSI.Keyboard.Backlight.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Device
{
    internal class HIDKeyboardDevice : IKeyboardDevice
    {
        private const int PRODUCT_ID = 0xFF00;
        private const int VENDOR_ID = 0x1770;

        public async Task<bool> ChangeColorAsync(Region region, Color color, Intensity intensity)
        {
            return await SendCommandAsync(new byte[] { 1, 2, 66, (byte)region, (byte)color, (byte)intensity, 0, 236 });
        }

        public async Task<bool> ChangeModeAsync(BlinkingMode blinkingMode)
        {
            return await SendCommandAsync(new byte[] { 1, 2, 65, (byte)blinkingMode, 0, 0, 0, 236 });
        }

        private Task<bool> SendCommandAsync(IEnumerable<byte> data)
        {
            var device = GetHidDevice();

            if (device is null)
            {
                throw new InvalidOperationException("MSI Keyboard not found, please ensure your keyboard is compatible.");
            }

            var result = device.WriteFeatureData(data.ToArray());

            return Task.FromResult(result);
        }

        private HidDevice GetHidDevice()
        {
            var device = HidDevices
                .Enumerate()
                .Where(s => s.Attributes.ProductId == PRODUCT_ID && s.Attributes.VendorId == VENDOR_ID)
                .FirstOrDefault();

            return device;
        }
    }
}
