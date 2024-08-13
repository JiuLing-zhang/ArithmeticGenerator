using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace ArithmeticGenerator.QuestionsCheck;
public class ExcelQuestionImporter : IQuestionImporter
{
    public List<List<string>> Import(string fileName)
    {
        List<List<string>> result = new List<List<string>>();

        IWorkbook workbook;
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        {
            if (fileName.EndsWith(".xls"))
            {
                workbook = new HSSFWorkbook(fileStream);
            }
            else if (fileName.EndsWith(".xlsx"))
            {
                workbook = new XSSFWorkbook(fileStream);
            }
            else
            {
                throw new NotSupportedException("不支持的文件格式");
            }
        }

        ISheet sheet = workbook.GetSheetAt(0);
        for (int i = 0; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            List<string> rowData = new List<string>();
            for (int j = 0; j < row.LastCellNum; j++)
            {
                ICell cell = row.GetCell(j);
                string cellValue = $"{cell}";
                rowData.Add(cellValue);
            }
            result.Add(rowData);
        }

        return result;
    }
}