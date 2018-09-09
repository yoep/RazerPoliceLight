using System.Collections.Generic;
using System.Threading;
using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse.Effects;

namespace RazerPoliceLights.Effects
{
    public class MouseEffect : IEffect
    {
        private readonly IMouse _chromaMouse;
        private readonly List<Color> _colors = new List<Color>();
        private readonly List<Color> _invertedColors = new List<Color>();
        private Thread _thread;
        private bool _isEffectRunning;

        public MouseEffect()
        {
            _chromaMouse = Chroma.Instance.Mouse;
            Init();
        }

        public void Play()
        {
            _isEffectRunning = true;
            _thread = new Thread(() =>
            {
                var i = 0;

                while (_isEffectRunning)
                {
                    i++;
                    _chromaMouse.SetGrid(new CustomGrid(i % 2 == 0 ? _colors : _invertedColors));
                    Thread.Sleep(200);
                    if (i > 10)
                        i = 0;
                }
            }) {IsBackground = true};
            _thread.Start();
        }

        public void Stop()
        {
            _isEffectRunning = false;
            _chromaMouse.SetEffect(Effect.None);
        }

        private void Init()
        {
            for (var i = 0; i < 63; i++)
            {
                _colors.Add(i % 2 == 0 ? Color.Blue : Color.Red);
                _invertedColors.Add(i % 2 == 0 ? Color.Red : Color.Blue);
            }
        }
    }
}