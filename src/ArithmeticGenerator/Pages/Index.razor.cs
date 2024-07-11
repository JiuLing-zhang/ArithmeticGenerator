namespace ArithmeticGenerator.Pages;
public partial class Index
{
    [Inject]
    private AppSettings AppSettings { get; set; } = default!;
    [Inject]
    private QuestionConfig QuestionConfig { get; set; } = default!;

    [Inject]
    private SettingWriter SettingWriter { get; set; } = default!;

    [Inject]
    private UpdateHelper UpdateHelper { get; set; } = default!;

    [Inject]
    private IWindowTitleBar WindowTitleBar { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;


    public string SheetSelectItem
    {
        get
        {
            return QuestionConfig.Sheets?.FirstOrDefault(x => x.IsActive)?.Name ?? "";
        }
        set
        {
            if (QuestionConfig.Sheets != null)
            {
                foreach (var sheet in QuestionConfig.Sheets)
                {
                    if (sheet.Name == value)
                    {
                        sheet.IsActive = true;
                    }
                    else
                    {
                        sheet.IsActive = false;
                    }
                }
                SettingWriter.SaveQuestionConfig(QuestionConfig);
            }
        }
    }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (AppSettings.IsAutoCheckUpdate)
        {
            await UpdateHelper.DoAsync(true);
        }

        if (QuestionConfig.Sheets == null || QuestionConfig.Sheets.Count == 0)
        {
            await AddSheetAsync();
        }
    }
    private void Restart()
    {
        WindowTitleBar.Restart();
    }

    private void SaveSettings()
    {
        SettingWriter.SaveAppSetting(AppSettings);
    }

    private async Task AddSheetAsync()
    {
        var options = new DialogOptions
        {
            BackgroundClass = "my-custom-class",
            NoHeader = true
        };
        var dialog = await DialogService.ShowAsync<EditSheetName>("编辑题库", options);
        var result = await dialog.Result;

        if (result == null || result.Canceled)
        {
            // Snackbar.Add("操作取消", Severity.Normal);
            return;
        }

        var newName = ((result.Data ?? "").ToString() ?? "").Trim();
        if (newName.IsEmpty())
        {
            Snackbar.Add("非法名称", Severity.Error);
            return;
        }

        if (QuestionConfig.Sheets == null || QuestionConfig.Sheets.Count == 0)
        {
            QuestionConfig.Sheets = new List<SheetConfig>();
        }

        if (QuestionConfig.Sheets.Any(x => x.Name.ToLower() == newName.ToLower()))
        {
            Snackbar.Add("操作失败，名称已经存在", Severity.Error);
            return;
        }

        foreach (var sheet in QuestionConfig.Sheets)
        {
            sheet.IsActive = false;
        }
        QuestionConfig.Sheets.Add(new SheetConfig()
        {
            Name = newName,
            IsActive = true
        });
        SettingWriter.SaveQuestionConfig(QuestionConfig);
    }
    private async Task RenameSheetAsync()
    {
        var options = new DialogOptions
        {
            BackgroundClass = "my-custom-class",
            NoHeader = true
        };
        var dialog = await DialogService.ShowAsync<EditSheetName>("编辑题库", options);
        var result = await dialog.Result;

        if (result == null || result.Canceled)
        {
            // Snackbar.Add("操作取消", Severity.Normal);
            return;
        }

        var newName = ((result.Data ?? "").ToString() ?? "").Trim();
        if (newName.IsEmpty())
        {
            Snackbar.Add("非法名称", Severity.Error);
            return;
        }

        if (newName.ToLower() == SheetSelectItem.ToLower())
        {
            return;
        }

        if (QuestionConfig.Sheets == null || QuestionConfig.Sheets.Count == 0)
        {
            QuestionConfig.Sheets = new List<SheetConfig>();
        }

        if (QuestionConfig.Sheets.Any(x => x.Name.ToLower() == newName.ToLower()))
        {
            Snackbar.Add("操作失败，名称已经存在", Severity.Error);
            return;
        }

        QuestionConfig.Sheets.First(x => x.Name == SheetSelectItem).Name = newName;
        SettingWriter.SaveQuestionConfig(QuestionConfig);
        await InvokeAsync(StateHasChanged);
    }
    private async Task DeleteSheetAsync()
    {
        if (QuestionConfig.Sheets == null || QuestionConfig.Sheets.Count == 0)
        {
            return;
        }

        bool? result = await DialogService.ShowMessageBox(
         "删除",
         $"确认要删除题库【{SheetSelectItem}】吗？",
         yesText: "删除", cancelText: "取消");

        if (result == null || result.Value != true)
        {
            return;
        }

        QuestionConfig.Sheets.RemoveAll(x => x.Name == SheetSelectItem);

        if (QuestionConfig.Sheets.Any())
        {
            QuestionConfig.Sheets.First().IsActive = true;
        }
        SettingWriter.SaveQuestionConfig(QuestionConfig);
    }
}