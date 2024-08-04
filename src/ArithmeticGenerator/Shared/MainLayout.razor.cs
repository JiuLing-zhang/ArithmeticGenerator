namespace ArithmeticGenerator.Shared;
public partial class MainLayout
{
    private bool _isDarkMode;
    private MudThemeProvider _mudThemeProvider = default!;
    private MudTheme _customTheme = default!;
    private string _version = "";

    [Inject]
    private IWindowMoving WindowMoving { get; set; } = default!;

    [Inject]
    private AppSettings AppSettings { get; set; } = default!;

    [Inject]
    private SettingWriter SettingWriter { get; set; } = default!;

    [Inject]
    private IDialogService Dialog { get; set; } = default!;

    [Inject]
    private UpdateHelper UpdateHelper { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _version = $"v {AppBase.Version[..AppBase.Version.LastIndexOf('.')]}";

        _customTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#009688",
                AppbarBackground = "#FFFFFF",
                AppbarText = "#009688",
                Background = "#FFFFFF",
                Surface = "#FFFFFF",
                TextPrimary = "#000000",
                TextSecondary = "#757575",
                ActionDisabled = "#B0B0B0",
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#009688",
                AppbarBackground = "#333333",
                AppbarText = "#009688",
                Background = "#333333",
                Surface = "#333333",
                TextPrimary = "#FFFFFF",
                TextSecondary = "#B0B0B0",
                ActionDisabled = "#9E9E9E",
            }
        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetTheme();
        }
    }

    private async Task ChangeTheme()
    {
        if (AppSettings.AppThemeInt == 0)
        {
            AppSettings.AppThemeInt = 1;
        }
        else if (AppSettings.AppThemeInt == 1)
        {
            AppSettings.AppThemeInt = 2;
        }
        else
        {
            AppSettings.AppThemeInt = 0;
        }
        SettingWriter.SaveAppSetting(AppSettings);
        await SetTheme();
    }

    private async Task SetTheme()
    {
        switch (AppSettings.AppThemeInt)
        {
            case 0:
                _isDarkMode = await _mudThemeProvider.GetSystemPreference();
                break;
            case 1:
                _isDarkMode = false;
                break;
            case 2:
                _isDarkMode = true;
                break;
        }
        await InvokeAsync(StateHasChanged);
    }

    private void MouseDown()
    {
        WindowMoving.MouseDown();
    }

    private void MouseUp()
    {
        WindowMoving.MouseUp();
    }

    private async Task OpenPayDialog()
    {
        var noHeader = new DialogOptions()
        {
            NoHeader = true,
            BackgroundClass = "dialog-blurry",
            CloseOnEscapeKey = false,
        };
        await Dialog.ShowAsync<Pay>("", noHeader);
    }

    private void SaveSettings()
    {
        SettingWriter.SaveAppSetting(AppSettings);
    }
    private async Task CheckUpdateAsync()
    {
        await UpdateHelper.DoAsync(false);
    }
}