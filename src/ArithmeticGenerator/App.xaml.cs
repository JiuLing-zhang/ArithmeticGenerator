using System.Globalization;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System.Windows;
using Application = System.Windows.Application;
using System.Text.Json;
using ArithmeticGenerator.QuestionBuilder;

namespace ArithmeticGenerator;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static Mutex _mutex = default!;
    private WindowMain? _mainWindow;
    protected override void OnStartup(StartupEventArgs e)
    {
        _mutex = new Mutex(true, AppBase.FriendlyName);
        if (!_mutex.WaitOne(0, false))
        {
            System.Windows.MessageBox.Show("程序已经运行");
            Application.Current.Shutdown();
            return;
        }

        Environment.CurrentDirectory = Path.GetDirectoryName(AppBase.ExecutablePath);

        Init();
        base.OnStartup(e);
    }

    private void Init()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<SettingWriter>();
        services.AddSingleton<AppSettings>((_) =>
        {
            return GetConfig<AppSettings>(AppBase.AppSettingConfigPath);
        });
        services.AddSingleton<QuestionConfig>((_) =>
        {
            return GetConfig<QuestionConfig>(AppBase.QuestionConfigPath);
        });
        services.AddSingleton(LogManager.GetLogger());
        services.AddSingleton<UpdateHelper>();
        services.AddSingleton<WindowMain>();
        services.AddSingleton<QuestionFactory>();
        services.AddSingleton<QuestionExport>();
        services.AddSingleton<IWindowMoving, WindowMoving>();
        services.AddSingleton<IWindowTitleBar, WindowTitleBar>();
        services.AddLocalization();
        services.AddWpfBlazorWebView();
        services.AddBlazorWebViewDeveloperTools();
        services.AddMudServices();

        var sp = services.BuildServiceProvider();
        Resources.Add("services", sp);

        _mainWindow = sp.GetRequiredService<WindowMain>();
        Application.Current.MainWindow = _mainWindow;
        _mainWindow.Show();
    }

    private T GetConfig<T>(string fileName) where T : new()
    {
        if (!File.Exists(fileName))
        {
            return new T();
        }
        string json = File.ReadAllText(fileName);
        try
        {
            var obj = JsonSerializer.Deserialize<T>(json);
            return obj ?? new T();
        }
        catch (JsonException)
        {
            return new T();
        }
    }
}