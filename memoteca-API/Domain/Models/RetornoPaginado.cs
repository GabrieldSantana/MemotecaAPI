namespace Domain.Models;
public class RetornoPaginado<T>
{
    public int TotalRegistros { get; set; }
    public int Pagina { get; set; }
    public int QtdPagina { get; set; }
    public List<T> Registros { get; set; }
}
