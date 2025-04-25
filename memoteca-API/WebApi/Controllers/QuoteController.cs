using Application.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/pensamentos")]
public class QuoteController : ControllerBase
{
    private readonly IQuoteService _service;

    public QuoteController(IQuoteService service)
    {
        _service = service;
    }

    [HttpGet]
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

    [HttpGet("{id}")]
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

    [HttpGet("{pagina}/{quantidade}")]
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

    [HttpPost]
    public async Task<IActionResult> CriarQuote([FromBody] QuoteDTO dto)
    {
        try
        {
            var quoteCriado = await _service.InserirQuoteAsync<QuoteDTO, QuoteModel, QuoteValidator>(dto);

            return Ok(quoteCriado);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao criar quote: " + ex.Message);
            Console.WriteLine("StackTrace: " + ex.StackTrace);

            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarQuote(int id, [FromBody] UpdateQuoteDTO dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest(new { message = "O ID na URL não corresponde ao ID no corpo da requisição." });

            var quoteAtualizado = await _service.AtualizarQuoteAsync<UpdateQuoteDTO, QuoteModel, QuoteValidator>(dto);
            return Ok(quoteAtualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverQuote(int id)
    {
        try
        {
            var quote = await _service.RemoverQuoteAsync(id);
            if (quote == null)
                return NotFound(new { message = $"Pensamento com ID {id} não encontrado!" });
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
