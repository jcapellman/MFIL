using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.POIFS.Macros;

namespace MFIL.lib.Analyzers
{
    public class LegacyExcelAnalyzer : BaseAnalyzer
    {
        public override string Name => "Legacy Excel";

        private List<string> GetUrls(HSSFWorkbook workbook)
        {
            var urls = new List<string>();

            for (var x = 0; x < workbook.NumberOfSheets; x++)
            {
                var sheet = workbook.GetSheetAt(x);

                for (var y = sheet.FirstRowNum; y < sheet.LastRowNum; y++)
                {
                    var row = sheet.GetRow(y);

                    foreach (var cell in row.Cells.Where(cell => cell.StringCellValue.Length >= 2))
                    {
                        urls.AddRange(GetURLsFromString(cell.StringCellValue));
                    }
                }
            }

            return urls;
        }

        private static List<string> ExtractMacros(Stream fileStream)
        {
            var reader = new VBAMacroReader(new NPOIFSFileSystem(fileStream));

            var macros = reader.ReadMacros();
            
            reader.Close();

            return macros.Values.ToList();
        }

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                var workbook = new HSSFWorkbook(fileStream);

                AddAnalysis("Sheets", GetUrls(workbook));

                AddAnalysis("Macros", ExtractMacros(fileStream));
            }
            catch (Exception)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}