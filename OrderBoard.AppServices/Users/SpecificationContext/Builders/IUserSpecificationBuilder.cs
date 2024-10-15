using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Users.SpecificationContext.Builders
{
    public interface IUserSpecificationBuilder
    {
        ISpecification<EntUser> BuildLogin(string? login, string password);
        ISpecification<EntUser> BuildEmail(string? email, string password);
        ISpecification<EntUser> BuildCheckLogin(string? login);
        ISpecification<EntUser> BuildCheckEmail(string? email);
        
    }
}
