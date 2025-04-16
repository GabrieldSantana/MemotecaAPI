using System.Reflection.Metadata.Ecma335;

namespace Domain.Models;
public class Quote
{
    public int Id { get; set; }
    public string Pensamento { get;set; }
    public string Autor { get; set; }
    public int Modelo { get; set; }
}
