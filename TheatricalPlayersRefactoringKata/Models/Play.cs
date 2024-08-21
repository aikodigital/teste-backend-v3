namespace TheatricalPlayersRefactoringKata
{
    public class Play
    {
        private string v1;
        private string v2;
        private int v3;

        public string Name { get; set; }
        public int Lines { get; set; }
        public string Type { get; set; }

        public Play(string name, int lines, string type)
        {
            Name = name;
            Lines = lines;
            Type = type;
        }

        public Play(string v1, string v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }
}

