using System.Reflection.Metadata.Ecma335;

namespace Domain.Models;
public class Pensamento
{
    public int Id { get; set; }
    public string? Quote { get;set; }
    public string? Autor { get; set; }
    public int Modelo { get; set; }
}
