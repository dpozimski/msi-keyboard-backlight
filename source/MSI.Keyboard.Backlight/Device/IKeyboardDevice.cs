using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Enums;

namespace MSI.Keyboard.Backlight.Device
{
    public interface IKeyboardDevice
    {
        /// <summary>
        /// Change a color of the keyboard by specific region with intensity.
        /// </summary>
        /// <param name="region">The region of the keyboard</param>
        /// <param name="color">The color of the region's buttons</param>
        /// <param name="intensity">The intensity of the color. Range 0-100.</param>
        /// <exception cref="InvalidOperationException">Thrown when keyboard device not found</exception>
        Task<bool> ChangeColorAsync(Region region, System.Drawing.Color color, int intensity);

        /// <summary>
        /// Checks if the current device is supported.
        /// </summary>
        /// <returns>Device is supported</returns>
        bool IsDeviceSupported();

        /// <summary>
        /// Sets the mode of the leds blinking.
        /// </summary>
        /// <param name="blinkingMode">The specific blinking effect</param>
        /// <exception cref="InvalidOperationException">Thrown when keyboard device not found</exception>
        Task<bool> ChangeModeAsync(BlinkingMode blinkingMode);
    }
}
