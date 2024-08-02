using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
/// <summary>
/// 乘
/// </summary>
/// <param name="number1"></param>
/// <param name="number2"></param>
internal class QuestionMultiply(CustomNumber number1, CustomNumber number2) : MathQuestion(number1, number2)
{
    public override string GenerateQuestion(ResultRuleEnum resultRule)
    {
        decimal value1;
        decimal value2;

        if ((resultRule & ResultRuleEnum.IsNotOne) == ResultRuleEnum.IsNotOne)
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
        return $"{value1} x {value2} = ___";
    }
}