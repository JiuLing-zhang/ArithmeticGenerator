using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
/// <summary>
/// 乘
/// </summary>
/// <param name="number1"></param>
/// <param name="number2"></param>
internal class QuestionMultiply(CustomNumber number1, CustomNumber number2) : MathQuestion(OperatorEnum.Multiply, number1, number2)
{
    public override string GenerateQuestion(QuestionRule questionRule, bool resultUseUnderline)
    {
        decimal value1;
        decimal value2;

        if ((questionRule.ResultRule & ResultRuleEnum.ValueIsNotOne) == ResultRuleEnum.ValueIsNotOne)
        {
            do
            {
                value1 = CreateNumberValue(Number1);
                value2 = CreateNumberValue(Number2);
            }
            while (value1 == 1 || value2 == 1);
        }
        else
        {
            value1 = CreateNumberValue(Number1);
            value2 = CreateNumberValue(Number2);
        }
        return BuilderQuestion(value1, value2, resultUseUnderline);
    }
}