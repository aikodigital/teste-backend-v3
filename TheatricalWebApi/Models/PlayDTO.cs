using Newtonsoft.Json;


namespace TheatricalWebApi.Models;
public class PlayDTO
{
    public string Type { get; set; } //Comedy,Tragedy,Historical..
    public string Name { get; set; }
    public int Lines { get; set; }

    // Construtor sem parâmetros
    public PlayDTO() { }
}

