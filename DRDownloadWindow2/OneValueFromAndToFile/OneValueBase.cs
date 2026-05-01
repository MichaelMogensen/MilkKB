using System.IO;

namespace DRDownloadWindow2.OneValueFromAndToFile
{
    public abstract class OneValueBase
    {
        protected string SettingFile { get; set; }            

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="settingName"></param>
        protected OneValueBase(string settingName)
        {
            SettingFile = Path.Combine(Environment.CurrentDirectory, $"one_{settingName.ToLower()}_value.setting");
        }

        /// <summary>
        /// Read setting by reading one line only.
        /// </summary>
        /// <returns></returns>
        protected string ReadSettingFile()
        {
            if (File.Exists(SettingFile))
            {
                return File.ReadAllLines(SettingFile).FirstOrDefault() ?? string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Write setting by writing one line only.
        /// </summary>
        protected void WriteSettingFile(object value)
        {
            File.WriteAllText(SettingFile, value.ToString());
        }

    }
}

