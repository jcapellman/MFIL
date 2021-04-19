using System.Collections.Generic;
using System.IO;
using System.Linq;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using MFIL.lib.Analyzers.Base;
using MFIL.lib.Exceptions;

namespace MFIL.lib.Analyzers
{
    public class OpenXMLExcelAnalyzer : BaseOpenXMLAnalyzer
    {
        protected override List<string> ParsePart<T>(OpenXmlPackage document)
        {
            var parts = new List<T>();

            var workbookPartWorksheetParts = (document as SpreadsheetDocument)?.WorkbookPart.WorksheetParts;

            if (workbookPartWorksheetParts == null)
            {
                return parts.Select(a => a.Uri.ToString()).ToList();
            }

            foreach (var worksheet in workbookPartWorksheetParts)
            {
                var sheetParts = worksheet?.GetPartsOfType<T>().ToList();

                if (sheetParts != null)
                {
                    parts.AddRange(sheetParts);
                }
            }

            return parts.Select(a => a.Uri.ToString()).ToList();
        }

        public override string Name => "OpenXML Excel";

        protected override List<string> GetUrls(OpenXmlPackage package)
        {
            var spreadsheet = (SpreadsheetDocument) package;

            var urls = new List<string>();

            foreach (var worksheet in spreadsheet.WorkbookPart.WorksheetParts)
            {
                var sheet = worksheet.Worksheet;

                var rows = sheet.GetFirstChild<SheetData>().Elements<Row>();

                foreach (var cell in rows.SelectMany(row => row.Elements<Cell>().ToList()))
                {
                    if (cell.DataType == null)
                    {
                        continue;
                    }

                    var cellValue = cell.InnerText;

                    switch (cell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            var stringTable = spreadsheet.WorkbookPart.GetPartsOfType<SharedStringTablePart>()
                                .FirstOrDefault();

                            if (stringTable == null)
                            {
                                continue;
                            }

                            var cellText = stringTable.SharedStringTable.ElementAt(int.Parse(cellValue)).InnerText;

                            if (cellText.Length > 2)
                            {
                                urls.AddRange(GetURLsFromString(cellText));
                            }

                            break;
                    }
                }
            }

            return urls;
        }
        
        public override Dictionary<string, List<string>> Analyze(Stream fileStream)
        {
            try
            {
                using var spreadsheet = SpreadsheetDocument.Open(fileStream, false);

                AddResult<MacroSheetPart>(spreadsheet, "Macros");
                AddResult<ConnectionsPart>(spreadsheet, "External Connections");
                AddResult<VbaProjectPart>(spreadsheet, "VBA Project");
                AddResult<EmbeddedObjectPart>(spreadsheet, "Embedded Objects");
                AddResult<ImagePart>(spreadsheet, "Images");
                AddResult<CustomDataPart>(spreadsheet, "Custom Data");
                AddResult<VbaDataPart>(spreadsheet, "VBA Data");
                AddResult<VmlDrawingPart>(spreadsheet, "VML Drawing");
                
                var urls = GetUrls(spreadsheet);

                AddAnalysis("Cell URLS", urls);
            }
            catch (OpenXmlPackageException)
            {
                throw new InvalidFileException($"File is not a {Name}");
            }

            return GetAnalysis();
        }
    }
}