using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArithmeticGenerator.Enums;

namespace ArithmeticGenerator.Models;
public class OldQuestionConfig
{
    public List<OldSheetConfig>? Sheets { get; set; }
}

public class OldSheetConfig
{
    public string Name { get; set; } = "";
    public bool IsActive { get; set; }
    public List<OldDisplayExpression>? Expressions { get; set; }
}

public class OldDisplayExpression
{
    /// <summary>
    /// 第一位数
    /// </summary>
    public CustomNumber Number1 { get; set; }

    /// <summary>
    /// 运算符
    /// </summary>
    public OperatorEnum Operator { get; set; }

    /// <summary>
    /// 第二位数
    /// </summary>
    public CustomNumber Number2 { get; set; }

    /// <summary>
    /// 结果约束条件
    /// </summary>
    public ResultRuleEnum ResultRule { get; set; }

    /// <summary>
    /// 唯一键值
    /// </summary>
    public string Key => $"{Number1}_{Operator}_{Number2}_{ResultRule}";

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName => $"{Number1} {Operator.GetDescription()} {Number2}";

    /// <summary>
    /// 显示描述
    /// </summary>
    public string DisplayDescription => CheckResultRuleEnumValues(ResultRule);

    private string CheckResultRuleEnumValues(ResultRuleEnum result)
    {
        var descriptions = new List<string>();
        foreach (ResultRuleEnum rule in Enum.GetValues(typeof(ResultRuleEnum)))
        {
            if (result.HasFlag(rule))
            {
                descriptions.Add(rule.GetDescription());
            }
        }
        return string.Join(",", descriptions);
    }
}
