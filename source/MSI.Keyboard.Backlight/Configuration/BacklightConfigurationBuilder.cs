using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MSI.Keyboard.Backlight.Enums;
using MSI.Keyboard.Backlight.Utils;

[assembly: InternalsVisibleTo("MSI.Keyboard.Backlight.Tests")]
namespace MSI.Keyboard.Backlight.Configuration
{
    internal class BacklightConfigurationBuilder : IBacklightConfigurationBuilder
    {
        private readonly IColorEnumToRgbConverter _colorEnumToRgbConverter;
        private readonly Dictionary<Region, RegionBacklightConfiguration> _regionConfigurations;
        
        private BlinkingMode _blinkingMode;

        public BacklightConfigurationBuilder(IColorEnumToRgbConverter colorEnumToRgbConverter)
        {
            _regionConfigurations = new Dictionary<Region, RegionBacklightConfiguration>()
            {
                { Region.Start, new RegionBacklightConfiguration(System.Drawing.Color.Red, 100) },
                { Region.Center, new RegionBacklightConfiguration(System.Drawing.Color.Red, 100) },
                { Region.End, new RegionBacklightConfiguration(System.Drawing.Color.Red, 100) }
            };
            _blinkingMode = BlinkingMode.Normal;
            _colorEnumToRgbConverter = colorEnumToRgbConverter;
        }

        public IBacklightConfigurationBuilder ForAllRegions(BlinkingMode blinkingMode)
        {
            _blinkingMode = blinkingMode;

            return this;
        }

        public IBacklightConfigurationBuilder ForAllRegions(System.Drawing.Color color, int intensity)
        {
            foreach (var region in _regionConfigurations.Keys.ToList())
            {
                ForRegion(region, color, intensity);
            }

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
            var rgbColor = _colorEnumToRgbConverter.ToRgb(color);

            _regionConfigurations[region] = new RegionBacklightConfiguration(rgbColor, (int)intensity);

            return this;
        }

        public IBacklightConfigurationBuilder ForRegion(Region region, System.Drawing.Color color, int intensity)
        {
            if(intensity > 100)
            {
                throw new InvalidOperationException($"{nameof(intensity)} can be number between 0-100");
            }

            _regionConfigurations[region] = new RegionBacklightConfiguration(color, intensity);

            return this;
        }

        public BacklightConfiguration Build()
        {
            return new BacklightConfiguration(_regionConfigurations, _blinkingMode);
        }
    }
}