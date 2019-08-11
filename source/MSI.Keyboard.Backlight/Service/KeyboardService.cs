using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Device;
using MSI.Keyboard.Backlight.Enums;

namespace MSI.Keyboard.Backlight.Service
{
    internal class KeyboardService : IKeyboardService
    {
        private readonly IKeyboardDevice _keyboardDevice;
        private BacklightConfiguration _currentConfiguration;

        public KeyboardService(IKeyboardDevice keyboardDevice)
        {
            _keyboardDevice = keyboardDevice;
        }

        public async Task ApplyConfigurationAsync(BacklightConfiguration configuration)
        {
            await ApplyBlinkingModeAsync(configuration.BlinkingMode);
            await ApplyRegionConfigurations(configuration.RegionConfigurations);

            _currentConfiguration = configuration;
        }

        public bool IsDeviceSupported()
        {
            return _keyboardDevice.IsDeviceSupported();
        }

        public BacklightConfiguration GetCurrentConfiguration()
        {
            return _currentConfiguration;
        }

        private async Task ApplyRegionConfigurations(IReadOnlyDictionary<Region, RegionBacklightConfiguration> regionConfigurations)
        {
            foreach (var regionConfiguration in regionConfigurations)
            {
                var regionBacklightConfiguration = regionConfiguration.Value;
                var result = await _keyboardDevice.ChangeColorAsync(
                                      regionConfiguration.Key,
                                      regionBacklightConfiguration.Color,
                                      regionBacklightConfiguration.Intensity);

                if (!result)
                {
                    throw new InvalidProgramException($"Cannot apply region configuration for {regionConfiguration.Key}");
                }
            }
        }

        private async Task ApplyBlinkingModeAsync(BlinkingMode blinkingMode)
        {
            var result = await _keyboardDevice.ChangeModeAsync(blinkingMode);

            if (!result)
            {
                throw new InvalidProgramException("Cannot apply blinking mode");
            }
        }
    }
}
