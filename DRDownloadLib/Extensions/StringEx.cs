namespace DRDownloadLib.Extensions
{
    public static class StringEx
    {
        /// <summary>
        /// Ensure to write "null" on object == null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theObject"></param>
        /// <returns></returns>
        public static string? OrDefault<T>(this T theObject, string? default_ = "null") =>
            theObject is null ? default_ : theObject.ToString();
        public static string? OrNil<T>(this T theObject) =>
            OrDefault(theObject, "NIL");
        public static string? OrNull<T>(this T theObject) =>
            OrDefault(theObject, "null");

        public static string? TrimTagStart(this string value)
        {
            if (!value.Contains('>'))
            { return value; }

            var splitValue = value.Split('>');

            return splitValue[1];
        }

        public static string? TrimTagEnd(this string value)
        {
            if (!value.Contains('<'))
            { return value; }

            var splitValue = value.Split('<');

            return splitValue[0];
        }

    }
}

