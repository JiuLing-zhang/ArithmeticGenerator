using System.IO;
using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace ArithmeticGenerator.QuestionBuilder;
internal class QuestionExport(QuestionFactory questionFactory)
{
    public void Export(string fileName, ExportConfig config, List<QuestionExpression> expressions)
    {
        var questions = GenerateQuestions(config, expressions);

        switch (config.FileType)
        {

            case FileTypeEnum.CSV:
                ExportToCsv(fileName, questions, config);
                break;
            case FileTypeEnum.TXT:
                ExportToTxt(fileName, questions, config);
                break;
            case FileTypeEnum.XLS:
                ExportToXls(fileName, questions, config);
                break;
            case FileTypeEnum.XLSX:
                ExportToXlsx(fileName, questions, config);
                break;
            default:
                throw new NotSupportedException($"暂时不支持 {config.FileType} 类型的导出");
        }
    }

    private List<string> GenerateQuestions(ExportConfig config, List<QuestionExpression> expressions)
    {
        var questions = new List<string>();
        var random = new Random();
        int totalExpressions = expressions.Count;

        switch (config.TopicRule)
        {
            case TopicRuleEnum.EachQuestion:
                for (int i = 0; i < config.QuestionCount; i++)
                {
                    var expression = expressions[i % totalExpressions];
                    var question = questionFactory.Create(expression.Operator, expression.Number1, expression.Number2).GenerateQuestion(expression.ResultRule, config.ResultUseUnderline);
                    questions.Add(question);
                }
                break;
            case TopicRuleEnum.EachType:
                int baseCount = config.QuestionCount / totalExpressions;
                int extraCount = config.QuestionCount % totalExpressions;

                for (int i = 0; i < totalExpressions; i++)
                {
                    int count = baseCount + (i < extraCount ? 1 : 0);
                    for (int j = 0; j < count; j++)
                    {
                        var expression = expressions[i];
                        var question = questionFactory.Create(expression.Operator, expression.Number1, expression.Number2).GenerateQuestion(expression.ResultRule, config.ResultUseUnderline);
                        questions.Add(question);
                    }
                }
                break;
            case TopicRuleEnum.RandomButEvenly:
                baseCount = config.QuestionCount / totalExpressions;
                extraCount = config.QuestionCount % totalExpressions;
                var counts = new int[totalExpressions];

                for (int i = 0; i < config.QuestionCount; i++)
                {
                    int index;
                    do
                    {
                        index = random.Next(totalExpressions);
                    } while (counts[index] >= baseCount + (index < extraCount ? 1 : 0));
                    counts[index]++;

                    var expression = expressions[index];
                    var question = questionFactory.Create(expression.Operator, expression.Number1, expression.Number2).GenerateQuestion(expression.ResultRule, config.ResultUseUnderline);
                    questions.Add(question);
                }
                break;
            case TopicRuleEnum.Random:
                for (int i = 0; i < config.QuestionCount; i++)
                {
                    var expression = expressions[random.Next(totalExpressions)];
                    var question = questionFactory.Create(expression.Operator, expression.Number1, expression.Number2).GenerateQuestion(expression.ResultRule, config.ResultUseUnderline);
                    questions.Add(question);
                }
                break;
        }

        return questions;
    }



    private void ExportToCsv(string fileName, List<string> questions, ExportConfig config)
    {
        var csvContent = BuildCsvContent(questions, config);
        File.WriteAllText(fileName, csvContent, System.Text.Encoding.UTF8);
    }

    private string BuildCsvContent(List<string> questions, ExportConfig config)
    {
        var csvLines = new List<string>();

        for (int i = 0; i < questions.Count; i += config.QuestionsPerRow)
        {
            var row = new List<string>();

            for (int j = 0; j < config.QuestionsPerRow && i + j < questions.Count; j++)
            {
                if (config.IncludeSeq)
                {
                    row.Add((i + j + 1).ToString());
                }
                row.Add(questions[i + j]);
            }

            csvLines.Add(string.Join(",", row));
        }

        return string.Join(Environment.NewLine, csvLines);
    }

    private void ExportToTxt(string fileName, List<string> questions, ExportConfig config)
    {
        var txtContent = BuildTxtContent(questions, config);
        File.WriteAllText(fileName, txtContent, System.Text.Encoding.UTF8); // 使用UTF-8编码写入文件
    }
    private string BuildTxtContent(List<string> questions, ExportConfig config)
    {
        var txtLines = new List<string>();

        for (int i = 0; i < questions.Count; i += config.QuestionsPerRow)
        {
            var row = new List<string>();

            for (int j = 0; j < config.QuestionsPerRow && i + j < questions.Count; j++)
            {
                if (config.IncludeSeq)
                {
                    row.Add($"({i + j + 1})"); // 使用括号包围序号
                }
                row.Add(questions[i + j]);
            }

            txtLines.Add(string.Join("\t", row)); // 使用制表符分隔
        }

        return string.Join(Environment.NewLine, txtLines);
    }

    private void ExportToXls(string fileName, List<string> questions, ExportConfig config)
    {
        var workbook = new HSSFWorkbook();
        var sheet = workbook.CreateSheet("Sheet1");
        int numberOfColumns = config.IncludeSeq ? config.QuestionsPerRow * 2 : config.QuestionsPerRow;
        for (int col = 0; col < numberOfColumns; col++)
        {
            if (config.IncludeSeq && col % 2 == 0)
            {
                sheet.SetColumnWidth(col, 5 * 256); // 序号列宽度为5个字符
            }
            else
            {
                sheet.SetColumnWidth(col, 20 * 256); // 表达式列宽度为20个字符
            }
        }

        int rowNumber = 0;
        for (int i = 0; i < questions.Count; i += config.QuestionsPerRow)
        {
            var row = sheet.CreateRow(rowNumber++);
            int cellNumber = 0;

            for (int j = 0; j < config.QuestionsPerRow && i + j < questions.Count; j++)
            {
                if (config.IncludeSeq)
                {
                    var cell = row.CreateCell(cellNumber++);
                    cell.SetCellValue($"({i + j + 1})");
                }
                var questionCell = row.CreateCell(cellNumber++);
                questionCell.SetCellValue(questions[i + j]);
            }
        }

        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
            workbook.Write(fs);
        }
    }

    private void ExportToXlsx(string fileName, List<string> questions, ExportConfig config)
    {
        var workbook = new XSSFWorkbook();
        var sheet = workbook.CreateSheet("Questions");
        int numberOfColumns = config.IncludeSeq ? config.QuestionsPerRow * 2 : config.QuestionsPerRow;
        for (int col = 0; col < numberOfColumns; col++)
        {
            if (config.IncludeSeq && col % 2 == 0)
            {
                sheet.SetColumnWidth(col, 5 * 256); // 序号列宽度为5个字符
            }
            else
            {
                sheet.SetColumnWidth(col, 20 * 256); // 表达式列宽度为20个字符
            }
        }

        int rowNumber = 0;
        for (int i = 0; i < questions.Count; i += config.QuestionsPerRow)
        {
            var row = sheet.CreateRow(rowNumber++);
            int cellNumber = 0;

            for (int j = 0; j < config.QuestionsPerRow && i + j < questions.Count; j++)
            {
                if (config.IncludeSeq)
                {
                    var cell = row.CreateCell(cellNumber++);
                    cell.SetCellValue($"({i + j + 1})");
                }
                var questionCell = row.CreateCell(cellNumber++);
                questionCell.SetCellValue(questions[i + j]);
            }
        }

        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
            workbook.Write(fs);
        }
    }
}
