namespace Domain.Dtos;
public class PensamentoDTO
{
    public string Quote { get; set; }
    public string Autor { get; set; }
    public int Modelo { get; set; }
}

public class UpdatePensamentoDTO
{
    public int Id { get; set; }
    public string Quote { get; set; }
    public string Autor { get; set; }
    public int Modelo { get; set; }
}
