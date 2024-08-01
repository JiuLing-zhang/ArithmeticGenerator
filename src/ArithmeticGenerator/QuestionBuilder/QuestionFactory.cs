using ArithmeticGenerator.Enums;
using ArithmeticGenerator.Models;

namespace ArithmeticGenerator.QuestionBuilder;
internal class QuestionFactory
{
    public MathQuestion Create(OperatorEnum @operator, CustomNumber number1, CustomNumber number2)
    {
        return @operator switch
        {
            OperatorEnum.Add => new QuestionAdd(number1, number2),
            OperatorEnum.Subtract => new QuestionSubtract(number1, number2),
            OperatorEnum.Multiply => new QuestionMultiply(number1, number2),
            OperatorEnum.Divide => new QuestionDivide(number1, number2),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

