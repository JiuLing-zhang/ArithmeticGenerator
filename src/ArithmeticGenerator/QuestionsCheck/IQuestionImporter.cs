using System.Collections.Generic;

namespace ArithmeticGenerator.QuestionsCheck;
public interface IQuestionImporter
{
    List<List<string>> Import(string fileName);
}