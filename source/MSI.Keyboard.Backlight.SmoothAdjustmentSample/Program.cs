using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Enums;
using MSI.Keyboard.Backlight.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.SmoothAdjustmentSample
{
    class Program
    {
        public static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            Task.Run(async () => await DoWorkAsync(cts.Token));

            Console.WriteLine("Press any key to stop the program...");
            Console.ReadKey();

            cts.Cancel();
        }

        private static async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            var service = KeyboardServiceFactory.Create();
            var intensityLevel = 0;
            var incrementIntensityLevel = true;

            while (!cancellationToken.IsCancellationRequested)
            {
                if (intensityLevel == 100)
                    incrementIntensityLevel = false;
                else if (intensityLevel == 0)
                    incrementIntensityLevel = true;

                var configuration = BacklightConfigurationBuilderFactory.Create()
                    .ForAllRegions(System.Drawing.Color.Red, intensityLevel)
                    .Build();

                await service.ApplyConfigurationAsync(configuration);
                await Task.Delay(1);

                intensityLevel += incrementIntensityLevel ? 1 : -1;
            }
        }
    }
}
