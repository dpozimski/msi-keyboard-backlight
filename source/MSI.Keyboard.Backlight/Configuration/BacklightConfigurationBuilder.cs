using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MSI.Keyboard.Backlight.Enums;

[assembly: InternalsVisibleTo("MSI.Keyboard.Backlight.Tests")]
namespace MSI.Keyboard.Backlight.Configuration
{
    internal class BacklightConfigurationBuilder : IBacklightConfigurationBuilder
    {
        private readonly Dictionary<Region, RegionBacklightConfiguration> _regionConfigurations;
        private BlinkingMode _blinkingMode;

        public BacklightConfigurationBuilder()
        {
            _regionConfigurations = new Dictionary<Region, RegionBacklightConfiguration>()
            {
                { Region.Start, new RegionBacklightConfiguration(Color.Red, Intensity.High) },
                { Region.Center, new RegionBacklightConfiguration(Color.Red, Intensity.High) },
                { Region.End, new RegionBacklightConfiguration(Color.Red, Intensity.High) }
            };
            _blinkingMode = BlinkingMode.Normal;
        }

        public IBacklightConfigurationBuilder ForAllRegions(BlinkingMode blinkingMode)
        {
            _blinkingMode = blinkingMode;

            return this;
        }

        public IBacklightConfigurationBuilder ForAllRegions(Color color, Intensity intensity)
        {
            foreach (var region in _regionConfigurations.Keys.ToList())
            {
                ForRegion(region, color, intensity);
            }

            return this;
        }

        public IBacklightConfigurationBuilder ForRegion(Region region, Color color, Intensity intensity)
        {
            _regionConfigurations[region] = new RegionBacklightConfiguration(color, intensity);

            return this;
        }

        public BacklightConfiguration Build()
        {
            return new BacklightConfiguration(_regionConfigurations, _blinkingMode);
        }
    }
}