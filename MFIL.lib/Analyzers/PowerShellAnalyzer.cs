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
                for (var nPosition = 0; nPosition < fileStream.Length; nPosition++)
                {
                    var a = fileStream.ReadByte();

                    if (a is not (>= 0 and <= 127))
                    {
                        throw new InvalidFileException($"Not a {Name} file");
                    }
                }

                // TODO: Reasonably certain it is a text file do FE
            }
            catch (InvalidFileException)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}