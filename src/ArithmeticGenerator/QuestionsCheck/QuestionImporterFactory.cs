﻿using System;

namespace ArithmeticGenerator.QuestionsCheck;
public class QuestionImporterFactory
{
    public IQuestionImporter CreateImporter(string fileName)
    {
        if (fileName.EndsWith(".xls") || fileName.EndsWith(".xlsx"))
        {
            return new ExcelQuestionImporter();
        }
        else if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png") || fileName.EndsWith(".bmp"))
        {
            return new ImageQuestionImporter();
        }
        else
        {
            throw new NotSupportedException("不支持的文件格式");
        }
    }
}