using JiuLing.AutoUpgrade.Shared;
using JiuLing.AutoUpgrade.Shell;
using System.IO;
using JiuLing.AutoUpgrade.Shell.Enums;

namespace ArithmeticGenerator;
internal class UpdateHelper(AppSettings appSettings)
{
    public async Task DoAsync(bool isBackgroundCheck)
    {
        var autoUpgradePath = Resources.Resource.AutoUpgradePath;
        if (autoUpgradePath.IsEmpty())
        {
            return;
        }
        var theme = appSettings.AppThemeInt switch
        {
            0 => ThemeEnum.System,
            1 => ThemeEnum.Light,
            2 => ThemeEnum.Dark,
            _ => ThemeEnum.Light
        };
        var iconPath = @"wwwroot\icon.ico";
        if (!File.Exists(iconPath))
        {
            iconPath = "";
        }
        await UpgradeFactory.CreateHttpApp(autoUpgradePath)
            .SetUpgrade(builder =>
            {
                builder.WithBackgroundCheck(isBackgroundCheck)
                .WithTheme(theme)
                .WithSignCheck(true)
                .WithIcon(iconPath)
                .WithVersionFormat(VersionFormatEnum.MajorMinorBuild);
            })
            .RunAsync();
    }
}