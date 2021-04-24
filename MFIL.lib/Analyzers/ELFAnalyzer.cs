using System.Collections.Generic;
using System.IO;
using System.Linq;

using ELFSharp.ELF;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

namespace MFIL.lib.Analyzers
{
    public class ELFAnalyzer : BaseAnalyzer
    {
        public override string Name => "ELF";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                using var elfFile = ELFReader.Load(fileStream, false);

                AddAnalysis("Sections", elfFile.Sections.Select(a => a.Name).ToList());
            }
            catch (InvalidFileException)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}