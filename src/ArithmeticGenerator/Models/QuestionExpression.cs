using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;

/// <summary>
/// 题目等式
/// </summary>
/// <param name="number1">第一位数</param>
/// <param name="operator">运算符</param>
/// <param name="number2">第二位数</param>
/// <param name="questionRule">题目规则</param>
public class QuestionExpression(CustomNumber number1, OperatorEnum @operator, CustomNumber number2, QuestionRule questionRule) : BaseExpression(number1, @operator, number2, questionRule)
{
}