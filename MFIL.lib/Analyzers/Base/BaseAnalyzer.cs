using System.Collections.Generic;
using System.IO;

namespace MFIL.lib.Analyzers.Base
{
    public abstract class BaseAnalyzer
    {
        public abstract string Name { get; }

        private Dictionary<string, List<string>> Results = new();

        public abstract Dictionary<string, List<string>> Analyze(Stream fileStream);

        protected void AddAnalysis(string key, List<string> analysis) => Results.Add(key, analysis);

        protected Dictionary<string, List<string>> GetAnalysis() => Results;
    }
}