using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
/// <summary>
/// 减法
/// </summary>
/// <param name="number1"></param>
/// <param name="number2"></param>
internal class QuestionSubtract(CustomNumber number1, CustomNumber number2) : MathQuestion(OperatorEnum.Subtract, number1, number2)
{
    public override string GenerateQuestion(QuestionRule questionRule, bool resultUseUnderline)
    {
        decimal value1;
        decimal value2;

        if ((questionRule.ResultRule & ResultRuleEnum.GreaterThanZero) == ResultRuleEnum.GreaterThanZero)
        {
            if (number1.Part1Length < Number2.Part1Length)
            {
                return "不存在这样的等式！";
            }

            do
            {
                value1 = CreateNumberValue(Number1);
                value2 = CreateNumberValue(Number2);
            }
            while (value1 <= value2);
        }
        else
        {
            value1 = CreateNumberValue(Number1);
            value2 = CreateNumberValue(Number2);
        }
        return BuilderQuestion(value1, value2, resultUseUnderline);
    }
}