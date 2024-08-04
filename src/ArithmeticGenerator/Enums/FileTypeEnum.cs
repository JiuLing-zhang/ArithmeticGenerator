using System.ComponentModel;

namespace ArithmeticGenerator.Enums;
public enum FileTypeEnum
{
    [Description("CSV（.csv）")]
    CSV,
    [Description("文本（.txt）")]
    TXT,
    [Description("Excel（.xls）")]
    XLS,
    [Description("Excel（.xlsx）")]
    XLSX
}