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
    protected override string GenerateQuestionInner(QuestionRule questionRule)
    {
        while (true)
        {
            decimal value1;
            decimal value2;

            if ((questionRule.ResultRule & ResultRuleEnum.GreaterThanZero) == ResultRuleEnum.GreaterThanZero)
            {
                do
                {
                    value1 = CreateNumberValue(Number1);
                    value2 = CreateNumberValue(Number2);
                } while (value1 <= value2);
            }
            else
            {
                value1 = CreateNumberValue(Number1);
                value2 = CreateNumberValue(Number2);
            }

            var resultString = (value1 - value2).ToString();
            if (resultString.Length < questionRule.MinLength || resultString.Length > questionRule.MaxLength)
            {
                continue;
            }
            var resultUseUnderline = (questionRule.ResultRule & ResultRuleEnum.ResultUseUnderline) == ResultRuleEnum.ResultUseUnderline;
            return BuilderQuestion(value1, value2, resultUseUnderline);
        }
    }

    protected override bool QuestionRuleValid(QuestionRule questionRule)
    {
        if (questionRule.MinLength > questionRule.MaxLength)
        {
            return false;
        }
        if ((questionRule.ResultRule & ResultRuleEnum.GreaterThanZero) == ResultRuleEnum.GreaterThanZero)
        {
            if (number1.Part1Length < Number2.Part1Length)
            {
                return false;
            }
        }

        var maxResult = MaxNumber1 - MinNumber2;
        var minResult = MinNumber1 - MaxNumber2;

        if (questionRule.MaxLength < minResult.ToString().Length)
        {
            return false;
        }

        if (questionRule.MinLength > maxResult.ToString().Length)
        {
            return false;
        }
        return true;
    }
}