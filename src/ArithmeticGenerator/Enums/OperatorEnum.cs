using System.ComponentModel;

namespace ArithmeticGenerator.Enums;

/// <summary>
/// 操作运算符
/// </summary>
public enum OperatorEnum
{
    /// <summary>
    /// 加
    /// </summary>
    [Description("+")]
    Add,

    /// <summary>
    /// 减
    /// </summary>
    [Description("-")]
    Subtract,

    /// <summary>
    /// 乘
    /// </summary>
    [Description("×")]
    Multiply,

    /// <summary>
    /// 除
    /// </summary>
    [Description("÷")]
    Divide
}