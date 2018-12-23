using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse;
using Corale.Colore.Razer.Mouse.Effects;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Devices.Razer
{
    public class RazerMouseEffect : AbstractMouseEffect
    {
        private IMouse _chromaMouse;

        #region Constructors

        public RazerMouseEffect(IRage rage, ISettingsManager settingsManager, IColorManager colorManager)
            : base(rage, settingsManager, colorManager)
        {
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            if (IsDisabled)
                return;
            
            Rage.LogTrivialDebug("Initializing Chroma.Instance.Mouse...");
            _chromaMouse = Chroma.Instance.Mouse;

            if (_chromaMouse != null)
            {
                Rage.LogTrivialDebug("Initialization of Chroma.Instance.Mouse done");
            }
            else
            {
                Rage.LogTrivial("Chroma.Instance.Mouse could not be registered, do you have a Chroma supported mouse?");
            }
        }

        #endregion

        protected override void OnEffectTick(PatternRow playPattern)
        {
            if (_chromaMouse == null)
                return; //something probably went wrong during initialization, ignore this device effect playback
            
            var columnSize = Constants.MaxColumns / playPattern.TotalColumns;
            var startIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = startIndex + columnSize;
                var rowEndIndex = startIndex + columnSize;

                if (IsMismatchingLastEndIndex(playPattern, Constants.MaxColumns, patternColumn, columnEndIndex))
                    columnEndIndex = Constants.MaxColumns;
                if (IsMismatchingLastEndIndex(playPattern, Constants.MaxRows, patternColumn, columnEndIndex))
                    rowEndIndex = Constants.MaxRows;

                if (IsAnimateVerticallyEnabled)
                {
                    AnimateVertical(playPattern, startIndex, rowEndIndex, patternColumn);
                    startIndex = rowEndIndex;
                }
                else
                {
                    AnimateHorizontal(playPattern, startIndex, columnEndIndex, patternColumn);
                    startIndex = columnEndIndex;
                }
            }
        }

        protected override void OnEffectStop()
        {
            _chromaMouse?.SetStatic(new Static(Led.All, SettingsManager.Settings.ColorSettings.StandbyColor));
        }

        private void AnimateHorizontal(PatternRow playPattern, int startIndex, int endIndex, int patternColumn)
        {
            for (var row = 0; row < Constants.MaxRows; row++)
            {
                for (var column = startIndex; column < endIndex; column++)
                {
                    _chromaMouse[row, column] =
                        GetPlaybackColumnColor(playPattern, patternColumn);
                }
            }
        }

        private void AnimateVertical(PatternRow playPattern, int startIndex, int endIndex, int patternColumn)
        {
            for (var column = 0; column < Constants.MaxColumns; column++)
            {
                for (var row = startIndex; row < endIndex; row++)
                {
                    _chromaMouse[row, column] =
                        GetPlaybackColumnColor(playPattern, patternColumn);
                }
            }
        }
    }
}