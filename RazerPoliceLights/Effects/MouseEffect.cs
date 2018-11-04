using System.Collections.Generic;
using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse;
using Corale.Colore.Razer.Mouse.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLights.Effects
{
    public class MouseEffect : AbstractEffect, IMouseEffect
    {
        private readonly IMouse _chromaMouse;

        #region Constructors

        public MouseEffect(IRage rage, ISettingsManager settingsManager, IElsSettingsManager elsSettingsManager) 
            : base(rage, settingsManager, elsSettingsManager)
        {
            _chromaMouse = Chroma.Instance.Mouse;
        }

        #endregion

        #region Properties

        protected override List<EffectPattern> EffectPatterns => EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse);

        protected override bool IsDisabled => !_settingsManager.Settings.DeviceSettings.MouseSettings.IsEnabled;

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

                if (_settingsManager.Settings.DeviceSettings.MouseSettings.AnimateVertically)
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
            _chromaMouse.SetStatic(new Static(Led.All, _settingsManager.Settings.ColorSettings.StandbyColor));
        }

        protected override bool IsScanModeEnabled()
        {
            return _settingsManager.Settings.DeviceSettings.MouseSettings.IsScanEnabled;
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