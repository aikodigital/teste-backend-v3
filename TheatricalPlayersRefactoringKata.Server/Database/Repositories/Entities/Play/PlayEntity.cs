namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play
{
    public class PlayEntity
    {
        public required string Name { get; set; }
        public required int Lines { get; set; }
        public required string Type { get; set; }

        public PlayEntity()
        { }
    }
}