namespace ArithmeticGenerator.Shared;
public partial class MainLayout
{
    private bool _isDarkMode;
    private MudThemeProvider _mudThemeProvider = default!;
    private MudTheme _customTheme = default!;

    [Inject]
    private IWindowMoving WindowMoving { get; set; } = default!;

    [Inject]
    private AppSettings AppSettings { get; set; } = default!;

    [Inject]
    private SettingWriter SettingWriter { get; set; } = default!;

    [Inject]
    private IDialogService Dialog { get; set; } = default!;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _customTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#6EA1EF",
                Secondary = Colors.Green.Accent4,
                AppbarBackground = "#F5F5F5",
                AppbarText = "#6EA1EF",
                TableStriped = "#F5F5F5",
                TableHover = "#F1F6FD",
                DarkLighten = "#CACDD1"
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#6EA1EF",
                AppbarBackground = "#1111",
                AppbarText = "#6EA1EF",
                Background = "#333333",
                Surface = "#333333",
                TableStriped = "#292929",
                TableHover = "#182437",
                DarkLighten = "#494C50",
                Black = "#858585",
                TextPrimary = "#FFFFFF",
                OverlayDark = "#7575757A"
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
}