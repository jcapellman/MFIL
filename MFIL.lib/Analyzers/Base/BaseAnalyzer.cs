using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MFIL.lib.Analyzers.Base
{
    public abstract class BaseAnalyzer
    {
        public abstract string Name { get; }

        private Dictionary<string, List<string>> Results = new();

        public abstract Dictionary<string, List<string>> Analyze(Stream fileStream);

        protected void AddAnalysis(string key, List<string> analysis) => Results.Add(key, analysis);

        protected Dictionary<string, List<string>> GetAnalysis() => Results;

        protected static List<string> GetURLsFromString(string str)
        {
            var urls = new List<string>();

            var pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";

            var regEx = new Regex(pattern, RegexOptions.IgnoreCase);

            var matches = regEx.Matches(str);

            foreach (Match match in matches)
            {
                urls.Add(match.Value);
            }

            return urls;
        }
    }
}