using Application.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuoteController : ControllerBase
{
    private readonly IQuoteService _service;

    public QuoteController(IQuoteService service)
    {
        _service = service;
    }

    [HttpGet("pensamentos")]
    public async Task<IActionResult> ListarTodosQuotes()
    {
        try
        {
            var quotes = await _service.ListarQuotesAsync();
            return Ok(quotes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("pensamentos/{id}")]
    public async Task<IActionResult> RetornarQuoteId(int id)
    {
        try
        {
            var quote = await _service.RetornarQuoteAsync(id);
            return Ok(quote);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("pensamentos/{pagina}/{quantidade}")]
    public async Task<IActionResult> RetornarQuotesPaginado(int pagina, int quantidade)
    {
        try
        {
            var quotesPaginado = await _service.RetornoPaginadoQuotesAsync(pagina, quantidade);
            return Ok(quotesPaginado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("pensamentos")]
    public async Task<IActionResult> CriarQuote([FromBody] QuoteDTO dto)
    {
        try
        {
            var quoteCriado = await _service.InserirQuoteAsync<QuoteDTO, QuoteModel, QuoteValidator>(dto);
            return Ok(quoteCriado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("pensamentos")]
    public async Task<IActionResult> AtualizarQuote([FromBody] UpdateQuoteDTO dto)
    {
        try
        {
            var quoteAtualizado = await _service.AtualizarQuoteAsync<UpdateQuoteDTO, QuoteModel, QuoteValidator>(dto);
            return Ok(quoteAtualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("pensamentos/{id}")]
    public async Task<IActionResult> RemoverQuote(int id)
    {
        try
        {
            var quote = await _service.RemoverQuoteAsync(id);
            return Ok($"Pensamento {id} removido com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
