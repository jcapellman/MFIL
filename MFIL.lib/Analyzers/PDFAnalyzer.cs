using System;
using System.Collections.Generic;
using System.IO;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

using PdfSharp.Pdf.IO;

namespace MFIL.lib.Analyzers
{
    public class PDFAnalyzer : BaseAnalyzer
    {
        public override string Name => "PDF";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                var file = PdfReader.Open(fileStream, PdfDocumentOpenMode.ReadOnly);

                if (file == null)
                {
                    throw new InvalidFileException($"File is not a {Name}");
;               }
            }
            catch (Exception)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}