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
    /// 唯一键值
    /// </summary>
    public string Key => $"{Number1}_{Operator}_{Number2}_{QuestionRule.ResultRule}";

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName => $"{Number1} {Operator.GetDescription()} {Number2}";

    /// <summary>
    /// 显示描述
    /// </summary>
    public string DisplayDescription => CheckResultRuleEnumValues(QuestionRule.ResultRule);

    private string CheckResultRuleEnumValues(ResultRuleEnum result)
    {
        var descriptions = new List<string>();
        foreach (ResultRuleEnum rule in Enum.GetValues(typeof(ResultRuleEnum)))
        {
            if (result.HasFlag(rule))
            {
                descriptions.Add(rule.GetDescription());
            }
        }
        return string.Join(",", descriptions);
    }
}