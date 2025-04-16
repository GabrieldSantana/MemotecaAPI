using Domain.Models;

namespace Infrastructure.Interfaces;
public interface IQuoteRepository
{
    Task<RetornoPaginado<QuoteModel>> BuscarQuotesPagina(int pagina, int qtdRegistros);
    Task<List<QuoteModel>> BuscarTodosQuotes();
    Task<QuoteModel> BuscarQuoteId(int id);
    Task<bool> InserirQuote(QuoteModel quote);
    Task<bool> AtualizarQuote(QuoteModel quote);
    Task<bool> ExcluirQuote(int id);
}
