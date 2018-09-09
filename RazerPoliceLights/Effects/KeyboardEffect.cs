using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard.Effects;

namespace RazerPoliceLights.Effects
{
    public class KeyboardEffect : IEffect
    {
        private readonly IKeyboard _chromaKeyboard;

        public KeyboardEffect()
        {
            _chromaKeyboard = Chroma.Instance.Keyboard;
        }

        public void Play()
        {
        }

        public void Stop()
        {
            _chromaKeyboard.SetEffect(Effect.None);
        }
    }
}