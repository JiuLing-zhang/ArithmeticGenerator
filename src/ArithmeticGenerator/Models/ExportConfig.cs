using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;
public class ExportConfig
{
    public int QuestionCount { get; set; } = 100;
    public int QuestionsPerRow { get; set; } = 3;
    public bool IncludeSeq { get; set; } = true;
    public bool ResultUseUnderline { get; set; } = true;
    public TopicRuleEnum TopicRule { get; set; } = TopicRuleEnum.RandomButEvenly;
    public FileTypeEnum FileType { get; set; } = FileTypeEnum.CSV;
}