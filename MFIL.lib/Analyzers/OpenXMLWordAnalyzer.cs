using System.Collections.Generic;
using System.IO;

using DocumentFormat.OpenXml.Packaging;

using MFIL.lib.Analyzers.Base;

namespace MFIL.lib.Analyzers
{
    public class OpenXMLWordAnalyzer : BaseOpenXMLAnalyzer
    {
        public override string Name => "OpenXML Word";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                using var word = WordprocessingDocument.Open(fileStream, false);
                
            }
            catch (OpenXmlPackageException)
            {
                AddAnalysis("Error", new List<string> { "File is not an OpenXML Word Document" });
            }

            return GetAnalysis();
        }

        protected override List<string> GetUrls(OpenXmlPackage package) => new List<string>();
    }
}