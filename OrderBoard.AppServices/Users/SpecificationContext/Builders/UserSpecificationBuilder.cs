using OrderBoard.AppServices.Other.Hasher;
using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.AppServices.Users.SpecificationContext.Specifications;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Users.SpecificationContext.Builders
{
    public class UserSpecificationBuilder : IUserSpecificationBuilder
    {
        public ISpecification<EntUser> BuildCheckEmail(string? email)
        {
            return Specification<EntUser>.FromPredicate(user => user.Email == email);
        }

        public ISpecification<EntUser> BuildCheckLogin(string? login)
        {
            return Specification<EntUser>.FromPredicate(user => user.Login == login);
        }

        public ISpecification<EntUser> BuildEmail(string? email, string password)
        {
            password = CryptoHasher.GetBase64Hash(password);
            var specification = Specification<EntUser>.FromPredicate(user =>
                user.Password == password);
            specification = specification.And(new EmailSpecification(email));
            return specification;
        }

        public ISpecification<EntUser> BuildLogin(string? login, string password)
        {
            password = CryptoHasher.GetBase64Hash(password);
            var specification = Specification<EntUser>.FromPredicate(user =>
                user.Password == password);
            specification = specification.And(new loginSpecification(login));
            return specification;
        }
    }
}
