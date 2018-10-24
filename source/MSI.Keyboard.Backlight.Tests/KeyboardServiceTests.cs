using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Device;
using MSI.Keyboard.Backlight.Enums;
using MSI.Keyboard.Backlight.Service;
using NSubstitute;
using Xunit;

namespace MSI.Keyboard.Backlight.Tests
{
    public class KeyboardServiceTests
    {
        [Fact]
        public void ApplyConfigurationAsync_ChangeModeFail_ShouldThrowInvalidProgramException()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var configuration = fixture.Create<BacklightConfiguration>();
            var keyboardDevice = Substitute.For<IKeyboardDevice>();
            keyboardDevice.ChangeModeAsync(Arg.Any<BlinkingMode>()).Returns(false);
            var keyboardService = new KeyboardService(keyboardDevice);
            //act
            Func<Task> fun = async () => { await keyboardService.ApplyConfigurationAsync(configuration); };
            //asert
            fun.Should()
               .Throw<InvalidProgramException>();
        }

        [Fact]
        public void ApplyConfigurationAsync_ChangeColorFail_ShouldThrowInvalidProgramException()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var configuration = fixture.Create<BacklightConfiguration>();
            var keyboardDevice = Substitute.For<IKeyboardDevice>();
            keyboardDevice.ChangeColorAsync(Arg.Any<Region>(), Arg.Any<Color>(), Arg.Any<Intensity>()).Returns(false);
            var keyboardService = new KeyboardService(keyboardDevice);
            //act
            Func<Task> fun = async () => { await keyboardService.ApplyConfigurationAsync(configuration); };
            //asert
            fun.Should()
               .Throw<InvalidProgramException>();
        }

        [Fact]
        public async void ApplyConfigurationAsync_ServiceChangeColor_ShouldBeCalled()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var configuration = fixture.Create<BacklightConfiguration>();
            var keyboardDevice = Substitute.For<IKeyboardDevice>();
            keyboardDevice.ChangeColorAsync(Arg.Any<Region>(), Arg.Any<Color>(), Arg.Any<Intensity>()).Returns(true);
            keyboardDevice.ChangeModeAsync(Arg.Any<BlinkingMode>()).Returns(true);
            var keyboardService = new KeyboardService(keyboardDevice);
            //act
            await keyboardService.ApplyConfigurationAsync(configuration);
            //asert
            await keyboardDevice.Received()
                .ChangeColorAsync(Arg.Any<Region>(), Arg.Any<Color>(), Arg.Any<Intensity>());
        }

        [Fact]
        public async void ApplyConfigurationAsync_ServiceChangeMode_ShouldBeCalled()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var configuration = fixture.Create<BacklightConfiguration>();
            var keyboardDevice = Substitute.For<IKeyboardDevice>();
            keyboardDevice.ChangeColorAsync(Arg.Any<Region>(), Arg.Any<Color>(), Arg.Any<Intensity>()).Returns(true);
            keyboardDevice.ChangeModeAsync(Arg.Any<BlinkingMode>()).Returns(true);
            var keyboardService = new KeyboardService(keyboardDevice);
            //act
            await keyboardService.ApplyConfigurationAsync(configuration);
            //asert
            await keyboardDevice.Received()
                .ChangeModeAsync(Arg.Any<BlinkingMode>());
        }

        [Fact]
        public void GetCurrentConfiguration_IfConfigurationNotApplied_ShouldBeNull()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var configuration = fixture.Create<BacklightConfiguration>();
            var keyboardDevice = Substitute.For<IKeyboardDevice>();
            var keyboardService = new KeyboardService(keyboardDevice);
            //act
            var result = keyboardService.GetCurrentConfiguration();
            //asert
            result.Should()
                .BeNull();
        }

        [Fact]
        public async void GetCurrentConfiguration_IfConfigurationApplied_ShouldBeNull()
        {
            //arrange
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            var configuration = fixture.Create<BacklightConfiguration>();
            var keyboardDevice = Substitute.For<IKeyboardDevice>();
            keyboardDevice.ChangeColorAsync(Arg.Any<Region>(), Arg.Any<Color>(), Arg.Any<Intensity>()).Returns(true);
            keyboardDevice.ChangeModeAsync(Arg.Any<BlinkingMode>()).Returns(true);
            var keyboardService = new KeyboardService(keyboardDevice);
            //act
            await keyboardService.ApplyConfigurationAsync(configuration);
            var result = keyboardService.GetCurrentConfiguration();
            //asert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(configuration);
        }
    }
}
