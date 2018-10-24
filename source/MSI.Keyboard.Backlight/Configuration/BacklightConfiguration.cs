using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Enums;

namespace MSI.Keyboard.Backlight.Configuration
{
    public class BacklightConfiguration
    {
        public IReadOnlyDictionary<Region, RegionBacklightConfiguration> RegionConfigurations { get; }
        public BlinkingMode BlinkingMode { get; }

        public BacklightConfiguration(IReadOnlyDictionary<Region, RegionBacklightConfiguration> regionConfigurations, BlinkingMode blinkingMode)
        {
            RegionConfigurations = regionConfigurations;
            BlinkingMode = blinkingMode;
        }
    }
}
