﻿using ArithmeticGenerator.Models;

namespace ArithmeticGenerator;
public class QuestionConfig
{
    public string? Version { get; set; }
    public List<SheetConfig>? Sheets { get; set; }
}

public class SheetConfig
{
    public string Name { get; set; } = "";
    public bool IsActive { get; set; }
    public List<QuestionExpression>? Expressions { get; set; }
}

