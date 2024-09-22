using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.enums;
public interface IPrintStatementBase
{
     abstract string Print(Statement statement,PrintType printType);
     abstract string PrintToXML(Statement statement);
     abstract string PrintToText(Statement statement);
}