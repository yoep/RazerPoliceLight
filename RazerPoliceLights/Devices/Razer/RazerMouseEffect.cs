using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse;
using Corale.Colore.Razer.Mouse.Effects;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLights.Devices.Razer
{
    public class RazerMouseEffect : AbstractMouseEffect
    {
        private readonly IMouse _chromaMouse;

        #region Constructors

        public RazerMouseEffect(IRage rage, ISettingsManager settingsManager, IElsSettingsManager elsSettingsManager)
            : base(rage, settingsManager, elsSettingsManager)
        {
            rage.LogTrivialDebug("Initializing Chroma.Instance.Mouse...");
            _chromaMouse = Chroma.Instance.Mouse;
            rage.LogTrivialDebug("Initialization of Chroma.Instance.Mouse done");
        }

        #endregion

        protected override void OnEffectTick(PatternRow playPattern)
        {
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
            _chromaMouse.SetStatic(new Static(Led.All, SettingsManager.Settings.ColorSettings.StandbyColor));
        }

        private void AnimateHorizontal(PatternRow playPattern, int startIndex, int endIndex, int patternColumn)
        {
            for (var row = 0; row < Constants.MaxRows; row++)
            {
                for (var column = startIndex; column < endIndex; column++)
                {
                    _chromaMouse[row, column] =
                        GetPlaybackColumnColor(playPattern.ColorColumns.ElementAt(patternColumn), patternColumn);
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
                        GetPlaybackColumnColor(playPattern.ColorColumns.ElementAt(patternColumn), patternColumn);
                }
            }
        }
    }
}