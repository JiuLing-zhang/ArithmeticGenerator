using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;

/// <summary>
/// 等式
/// </summary>
/// <param name="number1">第一位数</param>
/// <param name="operator">运算符</param>
/// <param name="number2">第二位数</param>
/// <param name="questionRule">题目规则</param>
public class BaseExpression(CustomNumber number1, OperatorEnum @operator, CustomNumber number2, QuestionRule questionRule)
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
    public QuestionRule QuestionRule { get; set; } = questionRule;

}