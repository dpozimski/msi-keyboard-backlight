using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Configuration;

namespace MSI.Keyboard.Backlight.Service
{
    public interface IKeyboardService
    {
        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <returns>configuration</returns>
        BacklightConfiguration GetCurrentConfiguration();

        /// <summary>
        /// Apply the configuration.
        /// <exception cref="InvalidOperationException">Thrown when keyboard device not found</exception>
        /// <exception cref="InvalidProgramException">If unrecognized IO exception occured</exception>
        /// <param name="configuration">Configuration of backlight</param>
        Task ApplyConfigurationAsync(BacklightConfiguration configuration);

        /// <summary>
        /// Checks if the current device is supported.
        /// </summary>
        /// <returns>Device is supported</returns>
        bool IsDeviceSupported();
    }
}
