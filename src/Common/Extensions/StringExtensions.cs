using System.Text.RegularExpressions;

namespace Common.Extensions;

public static class StringExtensions
{
    extension(string source)
    {
        public string RemoveSpecialCharacters()
            => Regex.Replace(source, @"[^0-9a-zA-Z\._@+]", string.Empty);

        public string RemoveNonAlphaNumericCharacters()
            => source is null ? null : Regex.Replace(source, @"[^0-9a-zA-Z_@]", string.Empty);
    }
}
