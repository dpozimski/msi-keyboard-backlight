<div align="center">
    <img src="https://raw.githubusercontent.com/dpozimski/msi-keyboard-backlight/develop/images/keyboard.png"/>
    <h1>msi-keyboard-backlight</h1>
</div>

# Description

The C# package which allows to control the backlight of your MSI keyboard!

[![Nuget](https://img.shields.io/badge/NuGet-Release-brightgreen.svg)](https://www.nuget.org/packages/MSI.Keyboard.Backlight/) [![Build Status](https://damianpozimski.visualstudio.com/msi-keyboard-backlight/_apis/build/status/master_msi-keyboard-backlight?branchName=master)](https://damianpozimski.visualstudio.com/msi-keyboard-backlight/_build/latest?definitionId=11&branchName=master)

If you have a notebook from MSI with steel series keyboard you can control your keyboard directly from code!

This package allows you to change the blinking mode and the color of the leds with programmer friendly way - using configuration builder and simply service which contains method to apply your created configuration.

My idea was to use this library to set the keyboard backlight depends of windows taskbar color.

I was inspired to create library by watching this video https://www.youtube.com/watch?v=z_ESTHVDSSQ.

# Support

My library use https://github.com/mikeobrien/HidLibrary package to get the handler to keyboard which allows me to write input data directly to device.

# Limitations

Currently the project is tested only on Windows operating system.

# Info

Feel free to send some feature requests!

# Changelog

v1.0.6

* Setting RGB value to each region
* Setting intensity level to each region (0-100)

v1.0.5

* Initial public release

# Simple adjustment sample

```csharp
public static async Task Main(string[] args)
{
    var configuration = BacklightConfigurationBuilderFactory.Create()
        .ForAllRegions(BlinkingMode.Normal)
        .ForRegion(Region.Start, Color.Red, Intensity.High)
        .ForRegion(Region.Center, Color.Green, Intensity.High)
        .ForRegion(Region.End, Color.Blue, Intensity.High)
        .Build();

    var service = KeyboardServiceFactory.Create();

    await service.ApplyConfigurationAsync(configuration);
}
```

# Smooth adjustment sample

```csharp
public static async Task Main(string[] args)
{
    var random = new Random();
    var service = KeyboardServiceFactory.Create();
    var configuration = BacklightConfigurationBuilderFactory.Create()
        .ForAllRegions(color: System.Drawing.Color.Red, intensity: random.Next(0, 100))
        .Build();

    await service.ApplyConfigurationAsync(configuration);
}
```
