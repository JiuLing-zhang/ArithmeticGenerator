using System.IO;

namespace ArithmeticGenerator;
public class AppSettingWriter()
{
    public void Save(AppSettings appSettings)
    {
        var directory = Path.GetDirectoryName(AppBase.ConfigPath) ?? throw new ArgumentException("配置文件读取失败");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        string appConfigString = System.Text.Json.JsonSerializer.Serialize(appSettings);
        File.WriteAllText(AppBase.ConfigPath, appConfigString);
    }
}