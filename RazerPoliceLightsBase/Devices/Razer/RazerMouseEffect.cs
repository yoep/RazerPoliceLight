using Corale.Colore;
using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse;
using Corale.Colore.Razer.Mouse.Effects;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsRage.Effects.Colors;

namespace RazerPoliceLightsBase.Devices.Razer
{
    public class RazerMouseEffect : AbstractMouseEffect
    {
        private IMouse _chromaMouse;

        #region Constructors

        public RazerMouseEffect(INotification notification, ILogger logger, ISettingsManager settingsManager, IColorManager colorManager)
            : base(notification, logger, settingsManager, colorManager)
        {
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            if (IsDisabled)
                return;

            Logger.Debug("Initializing Chroma.Instance.Mouse...");
            _chromaMouse = Chroma.Instance.Mouse;

            if (_chromaMouse != null)
            {
                Logger.Debug("Initialization of Chroma.Instance.Mouse done");
            }
            else
            {
                Logger.Warn("Chroma.Instance.Mouse could not be registered, do you have a Chroma supported mouse?");
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
                    try
                    {
                        _chromaMouse[row, column] =
                            GetPlaybackColumnColor(playPattern, patternColumn);
                    }
                    catch (ColoreException ex)
                    {
                        Logger.Warn("Chroma SDK has raised an issue the mouse: " + ex.Message, ex);
                    }
                }
            }
        }

        private void AnimateVertical(PatternRow playPattern, int startIndex, int endIndex, int patternColumn)
        {
            for (var column = 0; column < Constants.MaxColumns; column++)
            {
                for (var row = startIndex; row < endIndex; row++)
                {
                    try
                    {
                        _chromaMouse[row, column] =
                            GetPlaybackColumnColor(playPattern, patternColumn);
                    }
                    catch (ColoreException ex)
                    {
                        Logger.Warn("Chroma SDK has raised an issue the mouse: " + ex.Message, ex);
                    }
                }
            }
        }
    }
}