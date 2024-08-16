using System.IO;

namespace ArithmeticGenerator;
public class SettingWriter()
{
    public void SaveAppSetting(AppSettings appSettings)
    {
        SaveToFile<AppSettings>(AppBase.AppSettingConfigPath, appSettings);
    }

    public void SaveQuestionConfig(QuestionConfig questionConfig)
    {
        SaveToFile<QuestionConfig>(AppBase.QuestionConfigPath, questionConfig);
    }

    private void SaveToFile<T>(string fileName, T obj)
    {
        var directory = Path.GetDirectoryName(fileName) ?? throw new ArgumentException("文件读取失败");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        string json = System.Text.Json.JsonSerializer.Serialize(obj);
        File.WriteAllText(fileName, json);
    }
}