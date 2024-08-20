public class PlayDto
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Lines { get; set; }

    public PlayDto(string name, string type, int lines)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Lines = lines;
    }
}
