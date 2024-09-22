using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public static class ExtractFormatterServiceFactory
{
    public static IFormatter Create(FileExtension type)
    {
        return type switch
        {
            FileExtension.Txt => new TextFormatterService(),
            FileExtension.Xml => new SpreadSheetFormatterService(),
            _ => throw new Exception("Unknown extension type"),
        };
    }
}
