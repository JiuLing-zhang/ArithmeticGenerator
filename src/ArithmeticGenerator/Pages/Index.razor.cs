namespace ArithmeticGenerator.Pages;
public partial class Index
{
    [Inject]
    private AppSettings AppSettings { get; set; } = default!;

    [Inject]
    private AppSettingWriter AppSettingWriter { get; set; } = default!;

    [Inject]
    private UpdateHelper UpdateHelper { get; set; } = default!;

    [Inject]
    private IWindowTitleBar WindowTitleBar { get; set; } = default!;


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (AppSettings.IsAutoCheckUpdate)
        {
            await UpdateHelper.DoAsync(true);
        }
    }
    private void Restart()
    {
        WindowTitleBar.Restart();
    }

    private void SaveSettings()
    {
        AppSettingWriter.Save(AppSettings);
    }

}