using System;
using System.Collections.Generic;
using System.IO;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

namespace MFIL.lib.Analyzers
{
    public class ImageAnalyzer : BaseAnalyzer
    {
        public override string Name => "Image";

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                using var img = System.Drawing.Image.FromStream(fileStream);
            } catch (OutOfMemoryException)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}