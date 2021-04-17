using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MFIL.lib.Analyzers.Base;

using NPOI.HSSF.UserModel;

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

        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                var workbook = new HSSFWorkbook(fileStream);

                AddAnalysis("Sheets", GetUrls(workbook));
            }
            catch (Exception)
            {
                AddAnalysis("Error", new List<string> { $"File is not a {Name} Document" });
            }

            return GetAnalysis();
        }
    }
}