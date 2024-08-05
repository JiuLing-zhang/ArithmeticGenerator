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
        Random rand = new Random();
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

                if (number1.Part2Length != Number2.Part2Length)
                {
                    return "不存在这样的等式！";
                }

                int integerPartDigits;
                int fractionalPartDigits;
                do
                {
                    var value = rand.Next(1, 10);
                    value1 = value2 * value;
                    (integerPartDigits, fractionalPartDigits) = GetDecimalDigits(value1);
                } while (integerPartDigits != Number1.Part1Length || fractionalPartDigits != Number1.Part2Length);
            }
            break;
        }
        while (true);
        return $"{value1} ÷ {value2} = ___";
    }

    private (int integerPartDigits, int fractionalPartDigits) GetDecimalDigits(decimal number)
    {
        string numberString = number.ToString();

        int decimalPointIndex = numberString.IndexOf('.');

        if (decimalPointIndex == -1)
        {
            return (numberString.Length, 0);
        }

        int integerPartDigits = decimalPointIndex;
        int fractionalPartDigits = numberString.Length - decimalPointIndex - 1;

        return (integerPartDigits, fractionalPartDigits);
    }
}