using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;
public class QuoteRepository : IQuoteRepository
{
    private readonly Func<IDbConnection> _connection;

    public QuoteRepository(Func<IDbConnection> connection)
    {
        _connection = connection;
    }

    public async Task<bool> AtualizarQuote(QuoteModel quote)
    {
        try
        {
            using (var connection = _connection())
            {
                string sql = "UPDATE QUOTES SET PENSAMENTO=@PENSAMENTO, AUTOR=@AUTOR, MODELO=@MODELO WHERE ID=@ID";
                var parametros = new
                {
                    PENSAMENTO = quote.Pensamento,
                    AUTOR = quote.Autor,
                    MODELO = quote.Modelo,
                    ID = quote.Id
                };

                var quoteAtualizado = await connection.ExecuteAsync(sql, parametros);

                // Verifica a quantidade de linhas afetadas, se for maior que 0 retorna true
                return quoteAtualizado > 0 ? true : false;
            }
        }
        catch (Exception ex) { throw; }
        
    }

    public async Task<QuoteModel> BuscarQuoteId(int id)
    {
        try
        {
            using (var connection = _connection())
            {
                var sql = "SELECT * FROM QUOTES WHERE ID = @Id";

                var quote = await connection.QuerySingleOrDefaultAsync<QuoteModel>(sql, new { Id = id });

                return quote;
            }


        }
        catch (Exception ex) { throw; }
    }

    public async Task<RetornoPaginado<QuoteModel>> BuscarQuotesPagina(int pagina, int qtdRegistros)
    {
        try
        {
            using (var connection = _connection())
            {
                string sql = "SELECT * FROM QUOTES ORDER BY ID OFFSET @OFFSET ROW FETCH NEXT @QUANTIDADE ROWS ONLY";

                var parametros = new
                {
                    OFFSET = (pagina - 1) * qtdRegistros,
                    QUANTIDADE = qtdRegistros
                };

                var quotes = await connection.QueryAsync<QuoteModel>(sql, parametros);

                var totalQuotes = "SELECT COUNT(*) FROM QUOTES";

                var retornoTotalQuotes = await connection.ExecuteScalarAsync<int>(totalQuotes);

                return new RetornoPaginado<QuoteModel>()
                {
                    Pagina = pagina,
                    QtdPagina = qtdRegistros,
                    TotalRegistros = retornoTotalQuotes,
                    Registros = quotes.ToList(),
                };
            }


        }
        catch { throw; }
    }

    public async Task<List<QuoteModel>> BuscarTodosQuotes()
    {
        try
        {
            using (var connection = _connection())
            {
                string sql = "SELECT * FROM QUOTES";
                var quotes = await connection.QueryAsync<QuoteModel>(sql);

                return quotes.ToList();
            }

        }
        catch { throw; }
    }

    public async Task<bool> ExcluirQuote(int id)
    {
        try
        {
            using (var connection = _connection())
            {
                string sql = $"DELETE FROM QUOTES WHERE ID={id}";

                var quoteExcluido = await connection.ExecuteAsync(sql);

                return quoteExcluido > 0 ? true : false;
            }

        }
        catch (Exception ex) { throw; }
    }

    public async Task<bool> InserirQuote(QuoteModel quote)
    {
        try
        {
            using (var connection = _connection())
            {
                string sql = "INSERT INTO QUOTES (PENSAMENTO, AUTOR, MODELO) VALUES (@PENSAMENTO, @AUTOR, @MODELO)";

                var parametros = new
                {
                    PENSAMENTO = quote.Pensamento,
                    AUTOR = quote.Autor,
                    MODELO = quote.Modelo
                };

                var quoteCadastrado = await connection.ExecuteAsync(sql, parametros);

                return quoteCadastrado > 0 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro em InserirQuote: " + ex.Message);
            Console.WriteLine("StackTrace: " + ex.StackTrace);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }
            throw;
        }
    }
}
