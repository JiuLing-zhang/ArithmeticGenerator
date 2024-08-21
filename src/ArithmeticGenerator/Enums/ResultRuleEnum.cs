using System.ComponentModel;

namespace ArithmeticGenerator.Enums;

/// <summary>
/// 结果规则
/// </summary>
[Flags]
public enum ResultRuleEnum
{
    /// <summary>
    /// 结果大于0
    /// </summary>
    [Description("结果大于0")]
    GreaterThanZero = 1,

    /// <summary>
    /// 结果可以整除
    /// </summary>
    [Description("结果可以整除")]
    IsInt = 2,

    /// <summary>
    /// 不乘除1
    /// </summary>
    [Description("不乘除1")]
    ValueIsNotOne = 4,

    /// <summary>
    /// 结果不等于1
    /// </summary>
    [Description("结果不等于1")]
    ResultIsNotOne = 8,

    /// <summary>
    /// 答案下划线
    /// </summary>
    [Description("答案下划线")]
    ResultUseUnderline = 16
}