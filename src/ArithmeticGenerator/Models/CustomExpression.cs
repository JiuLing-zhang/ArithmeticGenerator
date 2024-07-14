using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;
/// <summary>
/// 等式
/// </summary>
/// <param name="number1">第一位数</param>
/// <param name="operator">运算符</param>
/// <param name="number2">第二位数</param>
/// <param name="resultRule">结果约束条件</param>
public class CustomExpression(CustomNumber number1, OperatorEnum @operator, CustomNumber number2, ResultRuleEnum resultRule)
{
    /// <summary>
    /// 第一位数
    /// </summary>
    public CustomNumber Number1 { get; set; } = number1;

    /// <summary>
    /// 运算符
    /// </summary>
    public OperatorEnum Operator { get; set; } = @operator;

    /// <summary>
    /// 第二位数
    /// </summary>
    public CustomNumber Number2 { get; set; } = number2;

    /// <summary>
    /// 结果约束条件
    /// </summary>
    public ResultRuleEnum ResultRule { get; set; } = resultRule;

    /// <summary>
    /// 唯一键值
    /// </summary>
    public string Key => $"{Number1}_{Operator}_{Number2}_{ResultRule}";

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName => $"{Number1} {Operator.GetDescription()} {Number2}";
}