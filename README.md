# msi-keyboard-backlight

The C# package which allows to control the backlight of your MSI keyboard!

[![Build status](https://damianpozimski.visualstudio.com/msi-keyboard-backlight/_apis/build/status/master_msi-keyboard-backlight)](https://damianpozimski.visualstudio.com/msi-keyboard-backlight/_build/latest?definitionId=11)

# Description

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

# Sample

```csharp
public static async Task Main(string[] args)
{
    var configuration = BacklightConfigurationBuilderFactory.Create()
        .ForAllRegions(BlinkingMode.Breathe)
        .ForRegion(Region.Start, Color.Red, Intensity.High)
        .ForRegion(Region.Center, Color.Green, Intensity.High)
        .ForRegion(Region.End, Color.Blue, Intensity.High)
        .Build();

        var service = KeyboardServiceFactory.Create();

        await service.ApplyConfigurationAsync(configuration);
}
```