using System.Collections.Generic;
using System.IO;
using System.Linq;

using DocumentFormat.OpenXml.Packaging;

using MFIL.lib.Analyzers.Base;

namespace MFIL.lib.Analyzers
{
    public class OpenXMLWordAnalyzer : BaseOpenXMLAnalyzer
    {
        public override string Name => "OpenXML Word";

        private static List<string> ParsePart<T>(WordprocessingDocument document) where T : OpenXmlPart => 
            document.GetPartsOfType<T>().Select(a => a.Uri.ToString()).ToList();

        private void AddResult<T>(WordprocessingDocument document, string name) where T : OpenXmlPart
        {
            var result = ParsePart<T>(document);

            AddAnalysis(name, result);
        }

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                using var word = WordprocessingDocument.Open(fileStream, false);

                AddResult<HeaderPart>(word, "URLS");
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