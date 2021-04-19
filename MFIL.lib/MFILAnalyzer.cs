using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Container;
using MFIL.lib.Exceptions;

namespace MFIL.lib
{
    public class MFILAnalyzer
    {
        private readonly List<BaseAnalyzer> _analyzers;

        public MFILAnalyzer()
        {
            _analyzers = typeof(MFILAnalyzer).Assembly.GetTypes()
                .Where(a => (a.BaseType == typeof(BaseAnalyzer) || a.BaseType == typeof(BaseOpenXMLAnalyzer)) 
                    && !a.IsAbstract)
                .Select(b => (BaseAnalyzer) Activator.CreateInstance(b)).ToList();
        }

        public AnalysisContainer Analyze(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), "FileName argument was null");
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File {fileName} was not found");
            }

            using var stream = File.OpenRead(fileName);

            var container = new AnalysisContainer
            {
                Analysis = new Dictionary<string, List<string>>(),
                FileType = null,
                Scannable = false
            };

            foreach (var analyzer in _analyzers)
            {
                try
                {
                    container.Analysis = analyzer.Analyze(stream);

                    container.Scannable = true;
                    container.FileType = analyzer.Name;

                    break;
                }
                catch (InvalidFileException ife)
                {
                    // TODO: log ife
                }
                catch (Exception ex)
                {
                    // TODO: log ex
                }
            }

            return container;
        }
    }
}