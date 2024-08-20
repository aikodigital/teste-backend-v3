namespace TheatricalPlayersRefactoringKata;

public class Play
{
    public string Title { get; }
    public int Lines { get; }
    public string Category { get; }

    public Play(string title, int lines, string category)
    {
        Title = title;
        Lines = lines;
        Category = category;
    }
}