namespace TheatricalPlayersRefactoringKata.Models
{
    public class Play
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }


        public Play(string title, string category)
        {
            Title = title;
            Category = category;
        }


        public Play() { }
    }
}
