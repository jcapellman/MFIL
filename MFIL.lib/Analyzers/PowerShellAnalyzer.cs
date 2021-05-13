using System.Collections.Generic;
using System.IO;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

namespace MFIL.lib.Analyzers
{
    public class PowerShellAnalyzer : BaseAnalyzer
    {
        public override string Name => "PowerShell";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                // TODO: Call get-command and check that at least one match is found
            }
            catch (InvalidFileException ife)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}