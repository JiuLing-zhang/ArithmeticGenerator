using ArithmeticGenerator.Models;
using ArithmeticGenerator.QuestionBuilder;
using System.Diagnostics;
using System.IO;
using ArithmeticGenerator.Enums;
using ArithmeticGenerator.QuestionsCheck;

namespace ArithmeticGenerator.Pages;
public partial class Index
{
    [Inject]
    private AppSettings AppSettings { get; set; } = default!;
    [Inject]
    private QuestionConfig QuestionConfig { get; set; } = default!;

    [Inject]
    private QuestionExport QuestionExport { get; set; } = default!;

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

    [Inject]
    private QuestionImporterFactory QuestionImporterFactory { get; set; } = default!;

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

    private List<DisplayExpression>? Expressions => QuestionConfig.Sheets?.FirstOrDefault(x => x.IsActive)?.Expressions?.OrderBy(x => x.DisplayName).ToList();

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

    private void OnExpressionSelected(DisplayExpression expression)
    {
        if (QuestionConfig.Sheets == null)
        {
            Snackbar.Add("保存失败，请选择题库", Severity.Error);
            return;
        }

        if (QuestionConfig.Sheets.First(x => x.Name == SheetSelectItem).Expressions == null)
        {
            QuestionConfig.Sheets.First(x => x.Name == SheetSelectItem).Expressions = new List<DisplayExpression>();
        }
        else
        {
            if (QuestionConfig.Sheets.First(x => x.Name == SheetSelectItem).Expressions.Any(x => x.Key == expression.Key))
            {
                Snackbar.Add("保存失败，题型已存在", Severity.Error);
                return;
            }
        }
        QuestionConfig.Sheets.First(x => x.Name == SheetSelectItem).Expressions.Add(expression);
        SettingWriter.SaveQuestionConfig(QuestionConfig);
        Snackbar.Add("保存成功", Severity.Success);
    }

    private void ExpressionRemove(MudChip<string> chip)
    {
        if (QuestionConfig.Sheets == null)
        {
            Snackbar.Add("操作失败，请选择题库", Severity.Error);
            return;
        }

        if (QuestionConfig.Sheets.First(x => x.Name == SheetSelectItem).Expressions == null)
        {
            return;
        }

        QuestionConfig.Sheets.First(x => x.Name == SheetSelectItem).Expressions.RemoveAll(x => x.Key == chip.Value);
        SettingWriter.SaveQuestionConfig(QuestionConfig);
    }

    private Task OnExport(ExportConfig exportConfig)
    {
        if (Expressions == null)
        {
            Snackbar.Add("导出失败，未能加载题型库", Severity.Error);
            return Task.CompletedTask;
        }
        var questionExpressions = new List<QuestionExpression>();
        foreach (var expression in Expressions)
        {
            questionExpressions.Add(new QuestionExpression(expression.Number1, expression.Operator, expression.Number2, expression.ResultRule));
        }
        var fileExt = exportConfig.FileType.ToString().ToLower();
        var fileName = $"ArithmeticGenerator_{DateTime.Now:yyyyMMdd_HHmmss}.{fileExt}";
        fileName = Path.Combine(System.Environment.CurrentDirectory, fileName);
        QuestionExport.Export(fileName, exportConfig, questionExpressions);
        Snackbar.Add($"导出成功:{fileName}", Severity.Success, config =>
        {
            config.Action = "打开";
            config.ActionColor = MudBlazor.Color.Primary;
            config.Onclick = _ =>
            {
                OpenFile(exportConfig.FileType, fileName);
                return Task.CompletedTask;
            };
        });

        return Task.CompletedTask;
    }

    public void OpenFile(FileTypeEnum fileType, string fileName)
    {
        Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
    }

    private async Task CheckQuestionsAsync()
    {
        var ofd = new OpenFileDialog
        {
            //Filter = "文件类型|*.xls;*.xlsx;*.jpg;*.png;*.bmp|All Files|*.*"
            Filter = "文件类型|*.xls;*.xlsx"
        };

        ofd.ShowDialog();
        var fileName = ofd.FileName;
        if (fileName.IsEmpty())
        {
            return;
        }

        var questionImport = QuestionImporterFactory.CreateImporter(fileName);
        List<List<string>> importedData = questionImport.Import(fileName);
        var options = new DialogOptions
        {
            BackgroundClass = "my-custom-class",
            MaxWidth = MaxWidth.ExtraLarge,
            NoHeader = true,
            FullWidth = true,
            CloseOnEscapeKey = false
        };
        var parameters = new DialogParameters<HomeworkCorrecting> { { x => x.Questions, importedData } };

        await DialogService.ShowAsync<HomeworkCorrecting>("批改作业", parameters, options);
    }
}