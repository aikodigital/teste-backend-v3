// InvoiceDTO.cs


namespace TheatricalWebApi.Models;
public class InvoiceDTO
{

    public string Customer { get; set; }
    public List<PerformanceDTO> Performances { get; set; }

    // Construtor padrão para desserialização
    public InvoiceDTO() { }

    // Construtor com parâmetros
    public InvoiceDTO(string customer, List<PerformanceDTO> performances)
    {
        Customer = customer;
        Performances = performances;
    }
}
