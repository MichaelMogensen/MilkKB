namespace DRDownloadWindow.OneValueSettingFile
{
    public class SaveOneValueInASettingFile : OneValueSettingFileBase
    {
        /// <summary>
        /// Set setting name by using nameof(my_property).
        /// </summary>
        /// <param name="settingName"></param>
        public SaveOneValueInASettingFile(string settingName) : base(settingName)
        {

        }

        /// <summary>
        /// Try write setting as bool.
        /// </summary>
        /// <param name="_default"></param>
        /// <returns></returns>
        public void Value(bool value)
        {
            WriteSettingFile(value);
        }
    }
}

