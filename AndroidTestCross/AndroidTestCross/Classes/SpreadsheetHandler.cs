using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace AndroidTestCross.Classes;

public class SpreadsheetHandler
{
    private string path;
    private string password;
    private string sheetName;
    
    // Beter txt opslaan en byte shuffle
    
    /// <summary>
    /// Constructor, handle spreadsheets
    /// </summary>
    /// <param name="path">Path of wanted file</param>
    /// <param name="password">Password of wanted file</param>
    public SpreadsheetHandler(string path, string password = "")
    {
        this.path = path;
        this.password = password;
        sheetName = "Sheet1";
    }
    //Makes a spreadsheet with variables: filePath, fileName, archivePassword(if spreadsheet will be archived)
    //Possibly also removes file/spreadsheet with same name if it already exists
    /// <summary>
    /// Makes a spreadsheet using input of constructor
    /// </summary>
    public void createSpreadsheet()
    {
        //Make new spreadsheet
        
        SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);
        //Make new workbook
        WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();

        //Add password to spreadsheet
        WorkbookProtection workbookProtection = new WorkbookProtection
        {
            LockStructure = true,       
            LockWindows = true,
            WorkbookPassword = password
        };
        workbookPart.Workbook.WorkbookProtection = workbookProtection;
        
        //Add worksheets to workbook
        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        worksheetPart.Worksheet = new Worksheet(new SheetData());
        
        Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
        
        // Append a new worksheet and associate it with the workbook.
        Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = sheetName };
        sheets.Append(sheet);

        workbookPart.Workbook.Save();
        
        spreadsheetDocument.Dispose();
    }

    public void readSpreadsheet()
    {
        OpenSettings set = new OpenSettings();
        
        SpreadsheetDocument spreadDoc = SpreadsheetDocument.Open(path, true);
        WorkbookPart? workbookPart = spreadDoc.WorkbookPart;
        //Workbook? workbook = workbookPart.Workbook;
        WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
        SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
        Row row = sheetData.Elements<Row>().First();
        Cell cell = row.Elements<Cell>().First();
        System.Console.WriteLine(cell.CellValue.Text);
        spreadDoc.Dispose();
    }

    public void writeSpreadsheet()
    {
        
        
    }
}