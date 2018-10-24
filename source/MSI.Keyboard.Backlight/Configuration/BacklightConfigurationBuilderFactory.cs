using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Configuration
{
    public static class BacklightConfigurationBuilderFactory
    {
        public static IBacklightConfigurationBuilder Create()
        {
            return new BacklightConfigurationBuilder();
        }
    }
}
