using System.Drawing;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic.Enums;

namespace RazerPoliceLights.Devices.Corsair
{
    public class CorsairDeviceManager : IDeviceManager
    {
        public CorsairDeviceManager()
        {
            CueSDK.Initialize();
            CueSDK.UpdateMode = UpdateMode.Continuous;
        }

        private void test()
        {
            var keyboard = CueSDK.KeyboardSDK;
            keyboard.Brush = (SolidColorBrush) Color.Transparent;
        }
    }
}