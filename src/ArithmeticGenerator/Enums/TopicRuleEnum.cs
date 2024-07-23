using System.ComponentModel;

namespace ArithmeticGenerator.Enums;
public enum TopicRuleEnum
{
    /// <summary>
    /// 每个题型轮询
    /// </summary>
    [Description("每个题型轮询")]
    EachQuestion,

    /// <summary>
    /// 每类题型轮询
    /// </summary>
    [Description("每类题型轮询")]
    EachType,

    /// <summary>
    /// 随机但均分题型
    /// </summary>
    [Description("随机但均分题型")]
    RandomButEvenly,

    /// <summary>
    /// 完全随机
    /// </summary>
    [Description("完全随机")]
    Random
}