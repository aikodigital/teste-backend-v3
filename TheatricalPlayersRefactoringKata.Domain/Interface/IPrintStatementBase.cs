using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.enums;
public interface IPrintStatementBase
{
      string Print(Statement statement,PrintType printType);
      string PrintToXML(Statement statement);
      string PrintToText(Statement statement);
}