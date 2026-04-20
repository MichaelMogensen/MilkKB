namespace DRDownloadWindow2.Utilities
{
    public class ReplaceDisallowedFilenameCharacters
    {
        public string Filename { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="filename"></param>
        public ReplaceDisallowedFilenameCharacters(string filename)
        {
            Filename = filename;

            Substitute();
        }

        /// <summary>
        /// Substitute with ~ for now.
        /// </summary>
        private void Substitute()
        {
            var replaceAWithB = new Tuple<string, string>[]
            {
                new Tuple<string, string>("\\", ""), // TOTO: Find some good char to replace with.
                new Tuple<string, string>("/", ""),
                new Tuple<string, string>(":", ""),
                new Tuple<string, string>("*", ""),
                new Tuple<string, string>("?", ""),
                new Tuple<string, string>("<", ""),
                new Tuple<string, string>(">", ""),
                new Tuple<string, string>("\"", ""),
                new Tuple<string, string>("|", "")
            };

            foreach (var ab in replaceAWithB)
            {
                var a = ab.Item1;
                var b = ab.Item2;

                Filename = Filename.Replace(a, b);
            }
        }
    }
}

