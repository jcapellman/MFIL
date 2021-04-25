using System;
using System.Collections.Generic;
using System.IO;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

namespace MFIL.lib.Analyzers
{
    public class PEAnalyzer : BaseAnalyzer
    {
        public override string Name => "PE";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                var file = new PeNet.PeFile(fileStream);
            }
            catch (OutOfMemoryException)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}