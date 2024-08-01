using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
internal abstract class MathQuestion(CustomNumber number1, CustomNumber number2)
{
    public CustomNumber Number1 { get; } = number1;
    public CustomNumber Number2 { get; } = number2;

    public abstract string GenerateQuestion(ResultRuleEnum resultRule);

    internal decimal CreateNumberValue(CustomNumber number)
    {
        var value1 = JiuLing.CommonLibs.RandomUtils.GetOneByLength(number.Part1Length);

        if (number.Part2Length == 0)
        {
            return Convert.ToDecimal(value1);
        }
        var value2 = JiuLing.CommonLibs.RandomUtils.GetOneByLength(number.Part2Length);
        return Convert.ToDecimal($"{value1}.{value2}");
    }
}

