using Domain.Models;
using FluentValidation;

namespace Domain.Validators;
public class QuoteValidator : AbstractValidator<QuoteModel>
{
    public QuoteValidator()
    {
        RuleFor(x => x.Pensamento)
            .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório!")
            .MaximumLength(250).WithMessage("O campo '{PropertyName}' deve ter até {MaxLenght} caracteres!");

        RuleFor(x => x.Autor)
            .NotNull().NotEmpty().WithMessage("O campo '{PropertyName}' é obrigatório!")
            .MaximumLength(50).WithMessage("O campo '{PropertyName}' deve ter até {MaxLenght} caracteres!");

        RuleFor(x => x.Modelo)
            .NotNull().WithMessage("O campo '{PropertyName}' é obrigatório!")
            .InclusiveBetween(1, 3)
            .WithMessage($"O campo '{{PropertyName}}' deve ser um modelo válido!");

    }
}
