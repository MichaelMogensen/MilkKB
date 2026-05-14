using DRDownloadLib.Types;

namespace DRDownloadWindow.Utilities
{
    public class UIElementProps<T>
    {
        public T Value { get; set; }

        public EWarningLevel WarningLevel { get; set; } = EWarningLevel.info;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="warningLevel"></param>
        public UIElementProps(T value, EWarningLevel warningLevel = EWarningLevel.info)
        {
            Value = value;
            WarningLevel = warningLevel;
        }
    }
}

