using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSI.Keyboard.Backlight.Enums;

namespace MSI.Keyboard.Backlight.Configuration
{
    public interface IBacklightConfigurationBuilder
    {
        IBacklightConfigurationBuilder ForAllRegions(BlinkingMode blinkingMode);
        IBacklightConfigurationBuilder ForAllRegions(Color color, Intensity intensity);
        IBacklightConfigurationBuilder ForRegion(Region region, Color color, Intensity intensity);
        
        BacklightConfiguration Build();
    }
}
