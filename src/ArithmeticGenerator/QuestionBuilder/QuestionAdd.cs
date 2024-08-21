using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;

/// <summary>
/// 加法
/// </summary>
/// <param name="number1"></param>
/// <param name="number2"></param>
internal class QuestionAdd(CustomNumber number1, CustomNumber number2) : MathQuestion(OperatorEnum.Add, number1, number2)
{
    public override string GenerateQuestion(QuestionRule questionRule)
    {
        while (true)
        {
            var value1 = CreateNumberValue(Number1);
            var value2 = CreateNumberValue(Number2);

            var result = (value1 + value2).ToString();
            if (result.Length < questionRule.MinLength || result.Length > questionRule.MaxLength)
            {
                continue;
            }

            var resultUseUnderline = (questionRule.ResultRule & ResultRuleEnum.ResultUseUnderline) == ResultRuleEnum.ResultUseUnderline;
            return BuilderQuestion(value1, value2, resultUseUnderline);
        }
    }
}