using FluentValidation;
using FornecedoresApi.Domain.Model;
using FornecedoresApi.Domain.Util;

namespace FornecedoresApi.Domain.Entidade;

public class Fornecedor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }

    public void AtualizaFornecedor(FornecedorModel fornecedorModel)
    {
        Nome = String.IsNullOrWhiteSpace(fornecedorModel.Nome) ? fornecedorModel.Nome : Nome;
        Email = ValidaUtil.IsValidEmail(fornecedorModel.Email) ? fornecedorModel.Email : Email;
        Telefone = ValidaUtil.IsValidPhoneNumber(fornecedorModel.Telefone) ? fornecedorModel.Telefone : Telefone;
        Endereco = ValidaUtil.IsValidAddress(fornecedorModel.Endereco) ? fornecedorModel.Endereco : Endereco;
    }
}
public class FornecedorValidator : AbstractValidator<Fornecedor>
{
    public FornecedorValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório.");

        // Validação de e-mail usando o método personalizado
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .Must(email => ValidaUtil.IsValidEmail(email)).WithMessage("O e-mail fornecido não é válido.");

        // Validação de telefone usando o método personalizado
        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Must(phone => ValidaUtil.IsValidPhoneNumber(phone)).WithMessage("O telefone fornecido não é válido.");

        // Validação de endereço usando o método personalizado
        RuleFor(x => x.Endereco)
            .NotEmpty().WithMessage("O endereço é obrigatório.")
            .Must(address => ValidaUtil.IsValidAddress(address)).WithMessage("O endereço fornecido não é válido.");
    }
}
