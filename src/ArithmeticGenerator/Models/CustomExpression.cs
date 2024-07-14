using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;
/// <summary>
/// 等式
/// </summary>
public class CustomExpression
{
    /// <summary>
    /// 第一位数
    /// </summary>
    public CustomNumber Number1 { get; set; }

    /// <summary>
    /// 运算符
    /// </summary>
    public OperatorEnum Operator { get; set; }

    /// <summary>
    /// 第二位数
    /// </summary>
    public CustomNumber Number2 { get; set; }

    /// <summary>
    /// 结果约束条件
    /// </summary>
    public ResultRuleEnum ResultRule { get; set; }

    /// <param name="number1">第一位数</param>
    /// <param name="operator">运算符</param>
    /// <param name="number2">第二位数</param>
    /// <param name="resultRule">结果约束条件</param>
    public CustomExpression(CustomNumber number1, OperatorEnum @operator, CustomNumber number2, ResultRuleEnum resultRule)
    {
        Number1 = number1;
        Operator = @operator;
        Number2 = number2;
        ResultRule = resultRule;
    }
}