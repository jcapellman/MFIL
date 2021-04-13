using System;
using System.Collections.Generic;
using System.IO;

using MFIL.lib.Analyzers.Base;

namespace MFIL.lib.Analyzers
{
    public class LegacyExcelAnalyzer : BaseAnalyzer
    {
        public override string Name => "Legacy Excel";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                
            }
            catch (Exception)
            {
                AddAnalysis("Error", new List<string> { $"File is not a {Name} Document" });
            }

            return GetAnalysis();
        }
    }
}