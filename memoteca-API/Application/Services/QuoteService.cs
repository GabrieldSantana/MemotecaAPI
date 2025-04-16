using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Interfaces;

namespace Application.Services;
public class QuoteService : IQuoteService
{
    private readonly IQuoteRepository _repository;
    private readonly IMapper _mapper;

    public QuoteService(IQuoteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TOutputModel> AtualizarQuoteAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
        where TInputModel : class
        where TOutputModel : class
        where TValidator : AbstractValidator<QuoteModel>
    {
        try
        {
            var entity = _mapper.Map<QuoteModel>(inputModel);

            Validacao<TValidator>(entity);

            bool resposta = await _repository.AtualizarQuote(entity);

            if (!resposta)
                throw new Exception("Não foi possível atualizar o pensamento!");
            else
            {
                var outPutModel = _mapper.Map<TOutputModel>(entity);
                return outPutModel;
            }
        }
        catch { throw; }
    }

    public async Task<TOutputModel> InserirQuoteAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
        where TInputModel : class
        where TOutputModel : class
        where TValidator : AbstractValidator<QuoteModel>
    {
        try
        {
            var entity = _mapper.Map<QuoteModel>(inputModel);

            Validacao<TValidator>(entity);
            
            bool resposta = await _repository.InserirQuote(entity);

            if (!resposta)
                throw new Exception("Não foi possível inserir um pensamento!");
            else
            {
                var outPutModel = _mapper.Map<TOutputModel>(entity);
                return outPutModel;
            }
        }
        catch { throw; }
    }

    public Task<List<QuoteModel>> ListarQuotesAsync()
    {
        try
        {
            var quotes = _repository.BuscarTodosQuotes();

            if (quotes != null)
                throw new Exception("Ainda não há pensamentos!");
            else 
                return quotes;
        }
        catch { throw; }
    }

    public async Task<bool> RemoverQuoteAsync(int id)
    {
        try
        {
            var quoteRemovido = await _repository.ExcluirQuote(id);

            if (!quoteRemovido)
                throw new Exception("Não foi possível remover o pensamento!");
            else
                return true;
        }
        catch { throw; }
    }

    public Task<QuoteModel> RetornarQuoteAsync(int id)
    {
        try
        {
            var quote = _repository.BuscarQuoteId(id);

            if (quote != null)
                throw new Exception("O pensamento não existe!");
            else
                return quote;
        }
        catch { throw; }
    }
    public Task<RetornoPaginado<QuoteModel>> RetornoPaginadoQuotesAsync(int pagina, int quantidade)
    {
        try
        {
            return _repository.BuscarQuotesPagina(pagina, quantidade);
        }
        catch (Exception ex) { throw; }
    }

    private static void Validacao<TValidator>(QuoteModel entity) where TValidator : AbstractValidator<QuoteModel>
    {
        try
        {
            var validator = Activator.CreateInstance<TValidator>();
            var result = validator.Validate(entity);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(error => new string(error.ErrorMessage));
                var errorString = string.Join(Environment.NewLine, errors);

                throw new Exception(errorString);
            }
        }
        catch (Exception) { throw; }
    }
}
