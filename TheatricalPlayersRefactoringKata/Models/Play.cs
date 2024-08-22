namespace TheatricalPlayersRefactoringKata.Models
{
    public class Play
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }

        // Construtor com parâmetros
        public Play(string title, string category)
        {
            Title = title;
            Category = category;
        }

        // Construtor padrão
        public Play() { }
    }
}
