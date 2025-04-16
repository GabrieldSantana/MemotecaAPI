namespace Domain.Dtos;
public class QuoteDTO
{
    public string Pensamento { get; set; }
    public string Autor { get; set; }
    public int Modelo { get; set; }
}

public class UpdateQuoteDTO
{
    public int Id { get; set; }
    public string Pensamento { get; set; }
    public string Autor { get; set; }
    public int Modelo { get; set; }
}
