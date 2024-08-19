namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play
{
    public class PlayEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Lines { get; set; } = 0;
        public string Type { get; set; } = string.Empty;

        public PlayEntity()
        { }

        public PlayEntity(string name, int lines, string type)
        {
            Name = name;
            Lines = lines;
            Type = type;
        }
    }
}