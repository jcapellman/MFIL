using System;
using System.Collections.Generic;
using System.IO;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

namespace MFIL.lib.Analyzers
{
    public class RTFAnalyzer : BaseAnalyzer
    {
        public override string Name => "RTF";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                var file = new Aspose.Words.Document(fileStream);
            }
            catch (OutOfMemoryException)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}