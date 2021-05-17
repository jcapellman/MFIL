using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

namespace MFIL.lib.Analyzers
{
    public class PEAnalyzer : BaseAnalyzer
    {
        public override string Name => "PE";

        private string[] RansomwareFunctions = { "VirtualProtect", "TerminateProcess" };
        
        private string[] RegistryFunctions = { "RegCloseKey", "RegCreateKeyA", "RegDeleteKeyA", "RegSetValueA", "RegOpenKeyA", "RegSetValueA", "RegSetValueExA" };

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                var file = new PeNet.PeFile(fileStream);

                AddAnalysis("DLLImports", file.ImportedFunctions?.Select(a => a.DLL).ToList());

                AddAnalysis("ImportedFunctions", file.ImportedFunctions?.Select(a => a.Name).ToList());

                AddAnalysis("RansomwareFunctions", file.ImportedFunctions?.Where(a => RansomwareFunctions.Contains(a.Name)).Select(b => b.Name).ToList());

                AddAnalysis("RegistryFunctions", file.ImportedFunctions?.Where(a => RegistryFunctions.Contains(a.Name)).Select(b => b.Name).ToList());

                AddAnalysis("ExportedFunctions", file.ExportedFunctions?.Select(a => a.Name).ToList());

                AddAnalysis("SectionHeaders", file.ImageSectionHeaders?.Select(a => a.Name).ToList());

                AddAnalysis("ImageSectionHeaders", file.ImageSectionHeaders?.Select(a => a.Name).ToList());
            }
            catch (OutOfMemoryException)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}