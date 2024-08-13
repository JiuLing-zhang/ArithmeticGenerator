using System.Collections.Generic;

namespace ArithmeticGenerator.QuestionsCheck;
public class ImageQuestionImporter : IQuestionImporter
{
    public List<List<string>> Import(string fileName)
    {
        throw new NotImplementedException("目前暂不支持图片类型导入");
    }
}