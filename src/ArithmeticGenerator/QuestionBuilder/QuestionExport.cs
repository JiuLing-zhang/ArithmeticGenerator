using System.IO;
using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
internal class QuestionExport
{
    private readonly QuestionFactory questionFactory;

    public QuestionExport(QuestionFactory questionFactory)
    {
        this.questionFactory = questionFactory;
    }

    public void Export(string fileName, ExportConfig config, List<QuestionExpression> expressions)
    {
        var questions = GenerateQuestions(config, expressions);

        switch (config.FileType)
        {
            case FileTypeEnum.CSV:
                ExportToCsv(fileName, questions, config);
                break;
            default:
                throw new NotSupportedException($"暂时不支持 {config.FileType} 类型的导出");
        }
    }

    private List<string> GenerateQuestions(ExportConfig config, List<QuestionExpression> expressions)
    {
        var questions = new List<string>();
        var random = new Random();

        switch (config.TopicRule)
        {
            case TopicRuleEnum.EachQuestion:
                for (int i = 0; i < config.QuestionCount; i++)
                {
                    var expression = expressions[i % expressions.Count];
                    var question = questionFactory.Create(expression.Operator, expression.Number1, expression.Number2).GenerateQuestion(expression.ResultRule);
                    questions.Add(question);
                }
                break;
            case TopicRuleEnum.EachType:
                for (int i = 0; i < expressions.Count; i++)
                {
                    for (int j = 0; j < config.QuestionCount / expressions.Count; j++)
                    {
                        var expression = expressions[i];
                        var question = questionFactory.Create(expression.Operator, expression.Number1, expression.Number2).GenerateQuestion(expression.ResultRule);
                        questions.Add(question);
                    }
                }
                break;
            case TopicRuleEnum.RandomButEvenly:
                var counts = new int[expressions.Count];
                for (int i = 0; i < config.QuestionCount; i++)
                {
                    int index;
                    do
                    {
                        index = random.Next(expressions.Count);
                    } while (counts[index] >= config.QuestionCount / expressions.Count);
                    counts[index]++;

                    var expression = expressions[index];
                    var question = questionFactory.Create(expression.Operator, expression.Number1, expression.Number2).GenerateQuestion(expression.ResultRule);
                    questions.Add(question);
                }
                break;
            case TopicRuleEnum.Random:
                for (int i = 0; i < config.QuestionCount; i++)
                {
                    var expression = expressions[random.Next(expressions.Count)];
                    var question = questionFactory.Create(expression.Operator, expression.Number1, expression.Number2).GenerateQuestion(expression.ResultRule);
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
}
