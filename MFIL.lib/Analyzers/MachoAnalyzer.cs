using System.Collections.Generic;
using System.IO;

using ELFSharp.MachO;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

namespace MFIL.lib.Analyzers
{
    public class MachoAnalyzer : BaseAnalyzer
    {
        public override string Name => "Mach-o";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                var machoFile = MachOReader.Load(fileStream, false);
            }
            catch (InvalidFileException)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}