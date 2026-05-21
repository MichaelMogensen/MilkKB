namespace DRDownloadLib.Utilities
{
    public class UniqueFilenameInFolder
    {
        public string File { get; private set; }

        public UniqueFilenameInFolder(string file)
        {
            File = file;
            MakeUniqueInFolder();
        }

        /// <summary>
        /// Is file alone in this folder?
        /// </summary>
        /// <returns></returns>
        private void MakeUniqueInFolder()
        {
            while (System.IO.File.Exists(File))
            {
                File = NewName(File);
            }
        }

        private static string NewName(string file) =>
            Path.Combine(
                Path.GetDirectoryName(file) ?? "ingen_folder",
                $"{Path.GetFileNameWithoutExtension(file) ?? "intet_navn"}" +
                $" - samme_navn" +
                $"{Path.GetExtension(file) ?? "ingen_endelse"}");
    }
}

