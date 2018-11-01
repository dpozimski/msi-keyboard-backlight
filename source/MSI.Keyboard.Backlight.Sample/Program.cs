using System;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Enums;
using MSI.Keyboard.Backlight.Service;

namespace MSI.Keyboard.Backlight.Sample
{
    public class Program
    {
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
    }
}
