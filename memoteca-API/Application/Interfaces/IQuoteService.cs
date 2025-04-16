using Domain.Models;
using FluentValidation;

namespace Application.Interfaces;
public interface IQuoteService
{
    Task<TOutputModel> InserirQuoteAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
        where TInputModel : class
        where TOutputModel : class
        where TValidator : AbstractValidator<QuoteModel>;
    Task<TOutputModel> AtualizarQuoteAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
        where TInputModel : class
        where TOutputModel : class
        where TValidator : AbstractValidator<QuoteModel>;
    Task<List<QuoteModel>> ListarQuotesAsync();
    Task<QuoteModel> RetornarQuoteAsync(int id);
    Task<bool> RemoverQuoteAsync(int id);
}
