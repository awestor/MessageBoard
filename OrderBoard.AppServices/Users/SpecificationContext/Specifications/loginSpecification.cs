using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Users.SpecificationContext.Specifications
{
    public class loginSpecification : Specification<EntUser>
    {
        private readonly string? _login;

        public loginSpecification(string? login)
        {
            _login = login;
        }

        /// <inheritdoc />
        public override Expression<Func<EntUser, bool>> PredicateExpression =>
            user => user.Login == _login;
    }
}
