namespace DRDownloadWindow2.OneValueFromAndToFile
{
    public class OneValueFromFile : OneValueBase
    {
        /// <summary>
        /// Set setting name by using nameof(my_property).
        /// </summary>
        /// <param name="settingName"></param>
        public OneValueFromFile(string settingName) : base(settingName)
        {
            
        }

        /// <summary>
        /// Try read setting as bool or fallback to default.
        /// </summary>
        /// <param name="_default"></param>
        /// <returns></returns>
        public bool ValueOrDefault(bool _default)
        {
            if (bool.TryParse(ReadSettingFile(), out bool result))
            { return result; }

            return _default;
        }
    }
}

