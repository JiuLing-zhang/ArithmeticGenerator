using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;
/// <summary>
/// 等式
/// </summary>
/// <param name="number1">第一位数</param>
/// <param name="operator">运算符</param>
/// <param name="number2">第二位数</param>
/// <param name="resultRule">结果约束条件</param>
public class DisplayExpression(CustomNumber number1, OperatorEnum @operator, CustomNumber number2, ResultRuleEnum resultRule) : BaseExpression(number1, @operator, number2, resultRule)
{
    /// <summary>
    /// 唯一键值
    /// </summary>
    public string Key => $"{Number1}_{Operator}_{Number2}_{ResultRule}";

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName => $"{Number1} {Operator.GetDescription()} {Number2}";

    /// <summary>
    /// 显示描述
    /// </summary>
    public string DisplayDescription => CheckResultRuleEnumValues(ResultRule);

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