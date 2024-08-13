using System.IO;

namespace ArithmeticGenerator.QuestionsCheck;
internal class CsvQuestionImporter : IQuestionImporter
{
    public List<List<string>> Import(string fileName)
    {
        var result = new List<List<string>>();

        using var reader = new StreamReader(fileName);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var row = new List<string>(line.Split(','));
            result.Add(row);
        }
        return result;
    }
}