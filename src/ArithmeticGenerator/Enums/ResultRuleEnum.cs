using System.ComponentModel;

namespace ArithmeticGenerator.Enums;

/// <summary>
/// 结果规则
/// </summary>
[Flags]
public enum ResultRuleEnum
{
    /// <summary>
    /// 大于零
    /// </summary>
    [Description("结果大于0")]
    GreaterThanZero = 1,

    /// <summary>
    /// 整数
    /// </summary>
    [Description("结果可以整除")]
    IsInt = 2,

    /// <summary>
    /// 不乘除1
    /// </summary>
    [Description("不乘除1")]
    IsNotOne = 4
}