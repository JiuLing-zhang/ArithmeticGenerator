using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.QuestionsCheck;
internal class ExpressionValidator
{
    public QuestionResultEnum Validate(string question)
    {
        string cleanedQuestion = question.Replace(" ", "").Replace("_", "");

        string operatorString = $"{OperatorEnum.Add.GetDescription()}{OperatorEnum.Subtract.GetDescription()}{OperatorEnum.Multiply.GetDescription()}{OperatorEnum.Divide.GetDescription()}";
        var match = Regex.Match(cleanedQuestion, @$"^(\d+(\.\d+)?)\s*([{operatorString}])\s*(\d+(\.\d+)?)\s*=\s*(-?\d+(\.\d+)?|\d*)$");

        if (!match.Success)
        {
            return QuestionResultEnum.Invalid;
        }

        if (!double.TryParse(match.Groups[1].Value, out double leftOperand))
        {
            return QuestionResultEnum.Invalid;
        }

        if (!double.TryParse(match.Groups[4].Value, out double rightOperand))
        {
            return QuestionResultEnum.Invalid;
        }

        OperatorEnum op = GetOperatorEnumByDescription(match.Groups[3].Value);

        string resultString = match.Groups[6].Value;
        if (string.IsNullOrEmpty(resultString))
        {
            return QuestionResultEnum.Incorrect;
        }

        if (!double.TryParse(resultString, out double expectedResult))
        {
            return QuestionResultEnum.Incorrect;
        }

        double actualResult;
        try
        {
            actualResult = op switch
            {
                OperatorEnum.Add => leftOperand + rightOperand,
                OperatorEnum.Subtract => leftOperand - rightOperand,
                OperatorEnum.Multiply => leftOperand * rightOperand,
                OperatorEnum.Divide => leftOperand / rightOperand,
                _ => throw new InvalidOperationException("未知运算符")
            };
        }
        catch
        {
            return QuestionResultEnum.Incorrect;
        }

        return Math.Abs(actualResult - expectedResult) < 1e-10 ? QuestionResultEnum.Correct : QuestionResultEnum.Incorrect;
    }

    private OperatorEnum GetOperatorEnumByDescription(string description)
    {
        foreach (var field in typeof(OperatorEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null && attribute.Description == description)
            {
                return (OperatorEnum)field.GetValue(null);
            }
        }
        throw new InvalidOperationException("未知运算符");
    }
}
