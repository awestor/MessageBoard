using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;


namespace OrderBoard.AppServices.Users.SpecificationContext.Specifications
{
    public class EmailSpecification : Specification<EntUser>
    {
        private readonly string? _email;

        public EmailSpecification(string? email)
        {
            _email = email;
        }

        /// <inheritdoc />
        public override Expression<Func<EntUser, bool>> PredicateExpression =>
            user => user.Email == _email;
    }
}
