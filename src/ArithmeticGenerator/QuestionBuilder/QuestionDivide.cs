using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
/// <summary>
/// 除
/// </summary>
/// <param name="number1"></param>
/// <param name="number2"></param>
internal class QuestionDivide(CustomNumber number1, CustomNumber number2) : MathQuestion(number1, number2)
{
    public override string GenerateQuestion(ResultRuleEnum resultRule)
    {
        decimal value1;
        decimal value2;

        do
        {
            value1 = CreateNumberValue(Number1);
            value2 = CreateNumberValue(Number2);

            if ((resultRule & ResultRuleEnum.IsNotOne) == ResultRuleEnum.IsNotOne)
            {
                if (value2 == 1)
                {
                    continue;
                }
            }

            if ((resultRule & ResultRuleEnum.IsInt) == ResultRuleEnum.IsInt)
            {
                if (number1.Part1Length < Number2.Part1Length)
                {
                    return "不存在这样的等式！";
                }

                if (value1 % value2 != 0)
                {
                    continue;
                }
            }
            break;
        }
        while (true);
        return $"{value1} ÷ {value2} =";
    }
}