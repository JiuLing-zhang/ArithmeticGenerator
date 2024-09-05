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
    protected override string GenerateQuestionInner(QuestionRule questionRule)
    {
        while (true)
        {
            decimal value1;
            decimal value2;

            if ((questionRule.ResultRule & ResultRuleEnum.ValueIsNotOne) == ResultRuleEnum.ValueIsNotOne)
            {
                do
                {
                    value1 = CreateNumberValue(Number1);
                    value2 = CreateNumberValue(Number2);
                } while (value1 == 1 || value2 == 1);
            }
            else
            {
                value1 = CreateNumberValue(Number1);
                value2 = CreateNumberValue(Number2);
            }

            var resultString = (value1 * value2).ToString();
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

        var maxResult = MaxNumber1 * MaxNumber2;
        var minResult = MinNumber1 * MinNumber2;

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