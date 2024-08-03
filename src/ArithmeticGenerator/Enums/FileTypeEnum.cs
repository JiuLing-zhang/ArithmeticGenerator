using System.ComponentModel;

namespace ArithmeticGenerator.Enums;
public enum FileTypeEnum
{
    [Description("CSV 文件（.csv）")]
    CSV,
    [Description("文本文件（.txt）")]
    TXT,
    [Description("Excel 文件（.xls）")]
    XLS
}