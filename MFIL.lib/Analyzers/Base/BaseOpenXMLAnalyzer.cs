using System.Collections.Generic;

using DocumentFormat.OpenXml.Packaging;

namespace MFIL.lib.Analyzers.Base
{
    public abstract class BaseOpenXMLAnalyzer : BaseAnalyzer
    {
        protected abstract List<string> GetUrls(OpenXmlPackage package);

        protected abstract List<string> ParsePart<T>(OpenXmlPackage document) where T : OpenXmlPart;

        protected void AddResult<T>(OpenXmlPackage document, string name) where T : OpenXmlPart
        {
            var result = ParsePart<T>(document);

            AddAnalysis(name, result);
        }
    }
}