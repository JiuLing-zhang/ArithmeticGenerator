using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
internal abstract class MathQuestion(OperatorEnum @operator, CustomNumber number1, CustomNumber number2)
{
    protected CustomNumber Number1 { get; } = number1;
    protected CustomNumber Number2 { get; } = number2;

    protected int MinNumber1 => GetMinByDigits(number1.Part1Length);
    protected int MaxNumber1 => GetMaxByDigits(number1.Part1Length);
    protected int MinNumber2 => GetMinByDigits(number2.Part1Length);
    protected int MaxNumber2 => GetMaxByDigits(number2.Part1Length);

    public string GenerateQuestion(QuestionRule questionRule)
    {
        if (!QuestionRuleValid(questionRule))
        {
            return "";
        }
        return GenerateQuestionInner(questionRule);
    }
    protected abstract string GenerateQuestionInner(QuestionRule questionRule);
    protected abstract bool QuestionRuleValid(QuestionRule questionRule);

    protected decimal CreateNumberValue(CustomNumber number)
    {
        var value1 = JiuLing.CommonLibs.RandomUtils.GetOneByLength(number.Part1Length);

        if (number.Part2Length == 0)
        {
            return Convert.ToDecimal(value1);
        }
        var value2 = JiuLing.CommonLibs.RandomUtils.GetOneByLength(number.Part2Length);
        return Convert.ToDecimal($"{value1}.{value2}");
    }

    protected string BuilderQuestion(decimal value1, decimal value2, bool resultUseUnderline)
    {
        var underline = resultUseUnderline ? "___" : "";
        return $"{value1} {@operator.GetDescription()} {value2} = {underline}";
    }

    /// <summary>
    /// 获取指定位数的最小值
    /// </summary>
    private int GetMinByDigits(int digits)
    {
        return (int)Math.Pow(10, digits - 1);
    }

    /// <summary>
    /// 获取指定位数的最大值
    /// </summary>
    private int GetMaxByDigits(int digits)
    {
        return (int)Math.Pow(10, digits) - 1;
    }
}

