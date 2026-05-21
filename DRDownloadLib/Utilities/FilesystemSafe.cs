namespace DRDownloadLib.Utilities
{
    public class FilesystemSafe
    {
        public string File { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file"></param>
        public FilesystemSafe(string file)
        {
            File = MakeFilesystemSafe(file);
        }

        /// <summary>
        /// Make filesystem safe by replacing invalid chars with -.
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

