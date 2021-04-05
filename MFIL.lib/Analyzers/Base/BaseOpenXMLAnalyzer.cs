using System.Collections.Generic;

using DocumentFormat.OpenXml.Packaging;

namespace MFIL.lib.Analyzers.Base
{
    public abstract class BaseOpenXMLAnalyzer : BaseAnalyzer
    {
        protected abstract List<string> GetUrls(OpenXmlPackage package);
    }
}