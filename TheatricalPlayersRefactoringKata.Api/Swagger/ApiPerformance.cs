using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheatricalPlayersRefactoringKata.Api.Swagger;

public class ApiPerformance
{   
    /// <summary>
    /// ID chave primária (PK)
    /// </summary>
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// A peça.
    /// </summary>
    [JsonPropertyName("Play")]
    public required string Play { get; set; }

    /// <summary>
    /// Tipo de peça. Pode ser Comédia, Tragédia, Histórica...
    /// </summary>
    [JsonPropertyName("Genre")]
    public required string Genre { get; set; }

    /// <summary>
    /// Valor total da peça.
    /// </summary>
    [JsonPropertyName("AmountOwed")]
    public decimal AmountOwed { get; set; }
    
    /// <summary>
    /// Créditos adquiridos.
    /// </summary>
    
    [JsonPropertyName("Credits")]
    public uint EarnedCredits { get; set; }
    
    /// <summary>
    /// Número de espectadores.
    /// </summary>
    [JsonPropertyName("Audience")]
    public uint Audience { get; set; }
}
