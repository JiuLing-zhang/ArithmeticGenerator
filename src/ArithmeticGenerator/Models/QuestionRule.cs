using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;
/// <summary>
/// 题目规则
/// </summary>
public class QuestionRule
{
    public ResultRuleEnum ResultRule { get; set; }
    /// <summary>
    /// 整数最大位数
    /// </summary>
    public int MaxLength { get; set; }
    /// <summary>
    /// 整数最小位数
    /// </summary>
    public int MinLength { get; set; }

    public override string ToString()
    {
        return $"{(int)ResultRule}_{MaxLength}_{MinLength}";
    }
}