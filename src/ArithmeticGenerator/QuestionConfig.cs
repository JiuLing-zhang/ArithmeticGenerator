using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticGenerator;
public class QuestionConfig
{
    public List<SheetConfig>? Sheets { get; set; }
}

public class SheetConfig
{
    public string Name { get; set; } = "";
    public bool IsActive { get; set; }
}