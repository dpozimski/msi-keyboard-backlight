using System;
using System.Collections.Generic;
using FluentAssertions;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Enums;
using Xunit;

namespace MSI.Keyboard.Backlight.Tests
{
    public class BacklightConfigurationBuilderTests
    {
        [Theory]
        [InlineData(Color.Blue, Intensity.High)]
        [InlineData(Color.Custom, Intensity.Medium)]
        [InlineData(Color.Off, Intensity.High)]
        [InlineData(Color.White, Intensity.High)]
        public void ForAllRegions_Build_ShouldSetTheSameConfiguration(Color color, Intensity intensity)
        {
            //arrange
            var backlightConfigurationBuilder = new BacklightConfigurationBuilder();
            //act
            backlightConfigurationBuilder.ForAllRegions(color, intensity);
            var result = backlightConfigurationBuilder.Build();
            //assert
            var expectedConfiguration = new RegionBacklightConfiguration(color, intensity);
            result.RegionConfigurations.Should()
                  .BeEquivalentTo(new Dictionary<Region, RegionBacklightConfiguration>()
                  {
                      { Region.Start, expectedConfiguration },
                      { Region.Center, expectedConfiguration },
                      { Region.End, expectedConfiguration }
                  });
        }

        [Fact]
        public void ForAllRegions_Build_ConfigurationShouldReturnRequestedBlinkingMode()
        {
            //arrange
            var backlightConfigurationBuilder = new BacklightConfigurationBuilder();
            //act
            backlightConfigurationBuilder.ForAllRegions(BlinkingMode.Breathe);
            var result = backlightConfigurationBuilder.Build();
            //assert
            result.BlinkingMode.Should()
                  .Equals(BlinkingMode.Breathe);
        }

        [Theory]
        [InlineData(Region.Start, Color.Blue, Intensity.High)]
        [InlineData(Region.Center, Color.Custom, Intensity.Medium)]
        [InlineData(Region.End, Color.Off, Intensity.High)]
        public void ForRegions_Build_ShouldSetConfigurationForSpecificRegion(Region region, Color color, Intensity intensity)
        {
            //arrange
            var backlightConfigurationBuilder = new BacklightConfigurationBuilder();
            //act
            backlightConfigurationBuilder.ForRegion(region, color, intensity);
            var result = backlightConfigurationBuilder.Build();
            //assert
            var expectedConfiguration = new RegionBacklightConfiguration(color, intensity);
            result.RegionConfigurations.Should()
                  .Contain(s => s.Value.Intensity == expectedConfiguration.Intensity && s.Value.Color == expectedConfiguration.Color);
        }

        [Fact]
        public void Build_ShouldAlwaysHaveThreeElements()
        {
            //arrange
            var backlightConfigurationBuilder = new BacklightConfigurationBuilder();
            //act
            var result = backlightConfigurationBuilder.Build();
            //assert
            result.RegionConfigurations.Should()
                  .HaveCount(3);
        }
    }
}
