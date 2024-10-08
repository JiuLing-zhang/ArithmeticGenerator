﻿using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
/// <summary>
/// 除
/// </summary>
/// <param name="number1"></param>
/// <param name="number2"></param>
internal class QuestionDivide(CustomNumber number1, CustomNumber number2) : MathQuestion(OperatorEnum.Divide, number1, number2)
{
    protected override string GenerateQuestionInner(QuestionRule questionRule)
    {
        decimal value1;
        decimal value2;
        Random rand = new Random();
        do
        {
            value1 = CreateNumberValue(Number1);
            value2 = CreateNumberValue(Number2);

            if ((questionRule.ResultRule & ResultRuleEnum.ValueIsNotOne) == ResultRuleEnum.ValueIsNotOne)
            {
                if (value2 == 1)
                {
                    continue;
                }
            }

            if ((questionRule.ResultRule & ResultRuleEnum.IsInt) == ResultRuleEnum.IsInt)
            {
                if (number1.Part1Length < Number2.Part1Length)
                {
                    return "不存在这样的等式！";
                }

                if (number1.Part2Length != Number2.Part2Length)
                {
                    return "不存在这样的等式！";
                }

                int result;
                // 如果位数相同，随机数小一点，更容易生成题目
                result = rand.Next(1, number1.Part1Length == Number2.Part1Length ? 4 : 10);

                value1 = value2 * result;
                var (integerPartDigits, fractionalPartDigits) = GetDecimalDigits(value1);

                if ((integerPartDigits != Number1.Part1Length || fractionalPartDigits != Number1.Part2Length))
                {
                    continue;
                }
            }

            if ((questionRule.ResultRule & ResultRuleEnum.ResultIsNotOne) == ResultRuleEnum.ResultIsNotOne)
            {
                if (value2 == value1)
                {
                    continue;
                }
            }

            var resultString = (value1 / value2).ToString();
            if (resultString.Length < questionRule.MinLength || resultString.Length > questionRule.MaxLength)
            {
                continue;
            }
            break;
        }
        while (true);

        var resultUseUnderline = (questionRule.ResultRule & ResultRuleEnum.ResultUseUnderline) == ResultRuleEnum.ResultUseUnderline;
        return BuilderQuestion(value1, value2, resultUseUnderline);
    }

    protected override bool QuestionRuleValid(QuestionRule questionRule)
    {
        if (questionRule.MinLength > questionRule.MaxLength)
        {
            return false;
        }

        if ((questionRule.ResultRule & ResultRuleEnum.IsInt) == ResultRuleEnum.IsInt)
        {
            if (number1.Part1Length < Number2.Part1Length)
            {
                return false;
            }

            if (number1.Part2Length != Number2.Part2Length)
            {
                return false;
            }
        }

        var maxResult = MaxNumber1 / MinNumber2;
        var minResult = MinNumber1 / MinNumber2;

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