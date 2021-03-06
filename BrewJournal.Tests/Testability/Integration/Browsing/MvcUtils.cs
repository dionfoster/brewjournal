using System;
using System.Text.RegularExpressions;

namespace BrewJournal.Tests.Testability.Integration.Browsing
{
    public static class MvcUtils
    {
        public static string ExtractAntiForgeryToken(string htmlResponseText)
        {
            if (htmlResponseText == null) throw new ArgumentNullException("htmlResponseText");

            var match = Regex.Match(htmlResponseText,
                @"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
            return match.Success ? match.Groups[1].Captures[0].Value : null;
        }
    }
}