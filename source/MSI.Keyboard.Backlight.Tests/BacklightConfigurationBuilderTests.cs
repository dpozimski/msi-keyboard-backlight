using System.Collections.Generic;
using FluentAssertions;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Enums;
using MSI.Keyboard.Backlight.Utils;
using NSubstitute;
using Xunit;

namespace MSI.Keyboard.Backlight.Tests
{
    public class BacklightConfigurationBuilderTests
    {
        public static IEnumerable<object[]> ColorIntensityTestCase = new List<object[]>
        {
            new object[] { System.Drawing.Color.Blue, 100 },
            new object[] { System.Drawing.Color.Black, 100 },
            new object[] { System.Drawing.Color.White, 100 }
        };

        public static IEnumerable<object[]> ColorIntensityEnumVersionTestCase = new List<object[]>
        {
            new object[] { Color.Blue, Intensity.High, System.Drawing.Color.Blue, 100 },
            new object[] { Color.Off, Intensity.Medium, System.Drawing.Color.Black, 75 },
            new object[] { Color.White, Intensity.Low, System.Drawing.Color.White, 50 }
        };

        public static IEnumerable<object[]> RegionColorIntensityTestCase = new List<object[]>
        {
            new object[] { Region.Start, System.Drawing.Color.Blue, 100 },
            new object[] { Region.Center, System.Drawing.Color.Green, 75 },
            new object[] { Region.End, System.Drawing.Color.Black, 50 }
        };

        public static IEnumerable<object[]> RegionColorIntensityEnumVersionTestCase = new List<object[]>
        {
            new object[] { Region.Start, Color.Blue, Intensity.High, System.Drawing.Color.Blue, 100 },
            new object[] { Region.Center, Color.Green, Intensity.Medium, System.Drawing.Color.Green, 75 },
            new object[] { Region.End, Color.Off, Intensity.Light, System.Drawing.Color.Black, 25 }
        };

        private readonly IColorEnumToRgbConverter _colorEnumToRgbConverterSubstitute;
        private readonly BacklightConfigurationBuilder _target;

        public BacklightConfigurationBuilderTests()
        {
            _colorEnumToRgbConverterSubstitute = Substitute.For<IColorEnumToRgbConverter>();
            _target = new BacklightConfigurationBuilder(_colorEnumToRgbConverterSubstitute);
        }

        [Theory]
        [MemberData(nameof(ColorIntensityTestCase))]
        public void ForAllRegions_Build_ShouldSetTheSameConfiguration(System.Drawing.Color color, int intensity)
        {
            //act
            _target.ForAllRegions(color, intensity);
            var result = _target.Build();
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

        [Theory]
        [MemberData(nameof(ColorIntensityEnumVersionTestCase))]
        public void ForAllRegions_EnumVersion_Build_ShouldSetTheSameConfiguration(Color color, Intensity intensity, System.Drawing.Color expectedColor, int expectedIntensity)
        {
            //arrange
            _colorEnumToRgbConverterSubstitute.ToRgb(Arg.Is(color)).Returns(expectedColor);
            //act
            _target.ForAllRegions(color, intensity);
            var result = _target.Build();
            //assert
            var expectedConfiguration = new RegionBacklightConfiguration(expectedColor, expectedIntensity);
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
            //act
            _target.ForAllRegions(BlinkingMode.Breathe);
            var result = _target.Build();
            //assert
            result.BlinkingMode.Should()
                  .Equals(BlinkingMode.Breathe);
        }

        [Theory]
        [MemberData(nameof(RegionColorIntensityTestCase))]
        public void ForRegions_Build_ShouldSetConfigurationForSpecificRegion(Region region, System.Drawing.Color color, int intensity)
        {
            //act
            _target.ForRegion(region, color, intensity);
            var result = _target.Build();
            //assert
            var expectedConfiguration = new RegionBacklightConfiguration(color, intensity);
            result.RegionConfigurations.Should()
                  .Contain(s => s.Value.Intensity == expectedConfiguration.Intensity && s.Value.Color == expectedConfiguration.Color);
        }

        [Theory]
        [MemberData(nameof(RegionColorIntensityEnumVersionTestCase))]
        public void ForRegions_EnumVersion_Build_ShouldSetConfigurationForSpecificRegion(Region region, Color color, Intensity intensity, System.Drawing.Color expectedColor, int expectedIntensity)
        {
            //arrange
            _colorEnumToRgbConverterSubstitute.ToRgb(Arg.Is(color)).Returns(expectedColor);
            //act
            _target.ForRegion(region, color, intensity);
            var result = _target.Build();
            //assert
            var expectedConfiguration = new RegionBacklightConfiguration(expectedColor, expectedIntensity);
            result.RegionConfigurations.Should()
                  .Contain(s => s.Value.Intensity == expectedConfiguration.Intensity && s.Value.Color == expectedConfiguration.Color);
        }

        [Fact]
        public void Build_ShouldAlwaysHaveThreeElements()
        {
            //act
            var result = _target.Build();
            //assert
            result.RegionConfigurations.Should()
                  .HaveCount(3);
        }
    }
}
