﻿using JiuLing.AutoUpgrade.Shared;
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
        await AutoUpgradeFactory.Create()
            .UseHttpMode(autoUpgradePath)
            .SetUpgrade(config =>
            {
                config.IsBackgroundCheck = isBackgroundCheck;
                config.Theme = theme;
                config.IsCheckSign = true;
                config.IconPath = iconPath;
                config.VersionFormat = VersionFormatEnum.MajorMinorBuild;
            })
            .RunAsync();
    }
}