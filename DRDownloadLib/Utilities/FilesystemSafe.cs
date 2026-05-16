namespace DRDownloadLib.Utilities
{
    public class FilesystemSafe
    {
        public string Result { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file"></param>
        public FilesystemSafe(string file)
        {
            Result = MakeFilesystemSafe(file);
        }

        /// <summary>
        /// Make filesystem safe.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string MakeFilesystemSafe(string file)
        {
            var id = 0;
            while ((id = file.IndexOfAny(Path.GetInvalidFileNameChars())) != -1)
            {
                file = file.Replace(file[id], '-');
            }

            return file;
        }

    }
}

