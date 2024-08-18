using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;
/// <summary>
/// 等式
/// </summary>
/// <param name="number1">第一位数</param>
/// <param name="operator">运算符</param>
/// <param name="number2">第二位数</param>
/// <param name="questionRule">结果约束条件</param>
public class DisplayExpression(CustomNumber number1, OperatorEnum @operator, CustomNumber number2, QuestionRule questionRule) : BaseExpression(number1, @operator, number2, questionRule)
{
    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName => $"{Number1} {Operator.GetDescription()} {Number2}";

    /// <summary>
    /// 显示描述
    /// </summary>
    public string DisplayDescription => BuildDescription();

    private string BuildDescription()
    {
        var descriptions = new List<string>
        {
            BuildResultRuleDescription(),
            BuildResultLengthDescription()
        };
        return string.Join(',', descriptions);
    }

    private string BuildResultRuleDescription()
    {
        var descriptions = new List<string>();
        foreach (ResultRuleEnum rule in Enum.GetValues(typeof(ResultRuleEnum)))
        {
            if (QuestionRule.ResultRule.HasFlag(rule))
            {
                descriptions.Add(rule.GetDescription());
            }
        }
        return string.Join(",", descriptions);
    }

    private string BuildResultLengthDescription()
    {
        var descriptions = new List<string>();
        if (QuestionRule.MinLength != 0)
        {
            descriptions.Add($"结果最小位数：{QuestionRule.MinLength}");
        }
        if (QuestionRule.MaxLength != 99999)
        {
            descriptions.Add($"结果最大位数：{QuestionRule.MaxLength}");
        }
        return string.Join(",", descriptions);
    }
}