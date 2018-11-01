using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MSI.Keyboard.Backlight.Utils;
using Xunit;

namespace MSI.Keyboard.Backlight.Tests
{
    public class ColorEnumToRgbConverterTests
    {
        [Fact]
        public void ToRgb_ShouldConvertToRedRgbColor()
        {
            //arrange
            var converter = new ColorEnumToRgbConverter();
            //act
            var result = converter.ToRgb(Enums.Color.Red);
            //assert
            result.Should().NotBeNull()
                  .And.BeEquivalentTo(Color.Red);
        }
    }
}
