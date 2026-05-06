using System.IO;
using DRDownloadWindow2.Utilities;

namespace DRDownloadWindow2.OneValueSettingFile
{
    public abstract class OneValueSettingFileBase
    {
        private static readonly string LockId = Util.GenerateRandomGuid();

        protected string SettingFile { get; set; }            

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="settingName"></param>
        protected OneValueSettingFileBase(string settingName)
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
                lock (LockId)
                {
                    return File.ReadAllLines(SettingFile).FirstOrDefault() ?? string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Write setting by writing one line only.
        /// </summary>
        protected void WriteSettingFile(object value)
        {
            lock (LockId)
            {
                File.WriteAllText(SettingFile, value.ToString());
            }
        }

    }
}

