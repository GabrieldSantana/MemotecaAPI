﻿using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories;
public class QuoteRepository : IQuoteRepository
{
    private readonly IDbConnection _connection;

    public QuoteRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> AtualizarQuote(QuoteModel quote)
    {
        try
        {
            string sql = "UPDATE QUOTES SET PENSAMENTO=@PENSAMENTO, AUTOR=@AUTOR, MODELO=@MODELO WHERE ID=@ID";
            var parametros = new
            {
                PENSAMENTO = quote.Pensamento,
                AUTOR = quote.Autor,
                MODELO = quote.Modelo,
                ID = quote.Id
            };

            var quoteAtualizado = await _connection.ExecuteAsync(sql, parametros);

            // Verifica a quantidade de linhas afetadas, se for maior que 0 retorna true
            return quoteAtualizado > 0 ? true : false;
        }
        catch (Exception ex) { throw; }
        
    }

    public async Task<QuoteModel> BuscarQuoteId(int id)
    {
        try
        {
            var sql = $"SELECT TOP 1 * FROM QUOTES WHERE ID = {id}";

            var quote = await _connection.QueryFirstOrDefaultAsync<QuoteModel>(sql);

            return quote;
        }
        catch (Exception ex) { throw; }
    }

    public async Task<List<QuoteModel>> BuscarTodosQuotes()
    {
        try
        {
            string sql = "SELECT * FROM QUOTES";
            var quotes = await _connection.QueryAsync<QuoteModel>(sql);

            return quotes.ToList();
        }
        catch (Exception ex) { throw; }
    }

    public async Task<bool> ExcluirQuote(int id)
    {
        try
        {
            string sql = $"DELETE FROM QUOTES WHERE ID={id}";

            var quoteExcluido = await _connection.ExecuteAsync(sql);

            return quoteExcluido > 0 ? true : false;
        }
        catch (Exception ex) { throw; }
    }

    public async Task<bool> InserirQuote(QuoteModel quote)
    {
        try
        {
            string sql = $"INSERT INTO QUOTES VALUES (@PENSAMENTO, @AUTOR, @MODELO)";

            var parametros = new
            {
                PENSAMENTO = quote.Pensamento,
                AUTOR = quote.Autor,
                MODELO = quote.Modelo
            };

            var quoteCadastrado = await _connection.ExecuteAsync(sql, parametros);

            return quoteCadastrado > 0 ? true : false;
        }
        catch (Exception ex) { throw; }
    }
}
