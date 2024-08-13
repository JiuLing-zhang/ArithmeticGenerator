using System.IO;

namespace ArithmeticGenerator.QuestionsCheck;
public class TxtQuestionImporter : IQuestionImporter
{
    public List<List<string>> Import(string fileName)
    {
        var result = new List<List<string>>();

        using var reader = new StreamReader(fileName);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var row = new List<string>(line.Split('\t'));
            result.Add(row);
        }
        return result;
    }
}