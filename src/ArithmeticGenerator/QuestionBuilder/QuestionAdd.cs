using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;

/// <summary>
/// 加法
/// </summary>
/// <param name="number1"></param>
/// <param name="number2"></param>
internal class QuestionAdd(CustomNumber number1, CustomNumber number2) : MathQuestion(number1, number2)
{
    public override string GenerateQuestion(ResultRuleEnum resultRule)
    {
        var value1 = CreateNumberValue(Number1);
        var value2 = CreateNumberValue(Number2);
        return $"{value1} ＋ {value2} = ___";
    }
}