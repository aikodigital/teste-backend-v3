using System.ComponentModel.DataAnnotations;

/// <summary>
/// Representa um modelo de domínio para performances teatrais.
/// </summary>
public class ApiTheatrical
{
    /// <summary>
    /// ID chave primária (PK)
    /// </summary>
    [Required]
    public required string Id { get; set; }

    /// <summary>
    /// O nome da peça teatral.
    /// </summary>
    [Required]
    [StringLength(100)]
    public required string Title { get; set; }

    /// <summary>
    /// Tipo de peça, pode ser Comédia, Tragédia, Histórica e etc.
    /// </summary>
    [Required]
    public required string Genre { get; set; }

    /// <summary>
    /// Duração da peça em minutos.
    /// </summary>
    [Range(30, 240)]
    public int DurationInMinutes { get; set; }

    /// <summary>
    /// Preço base da peça.
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser maior ou igual a zero")]
    public decimal BasePrice { get; set; }

    /// <summary>
    /// Data da apresentação.
    /// </summary>
    [Required]
    public DateTime PerformanceDate { get; set; }

    /// <summary>
    /// Número de espectadores.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Audience { get; set; }
}
