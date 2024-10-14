using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.BasePagination;
using OrderBoard.Contracts.Categories.Requests;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Categories.SpecificationContext.Builders
{
    public class CategorySpecificationBuilder : ICategorySpecificationBuilder
    {
        public ISpecification<Category> Build(SearchCategoryRequest request)
        {
            var specification = Specification<Category>.True;
            if (request.ParentId != null)
            {
                specification = Specification<Category>.FromPredicate(item => item.ParentId == request.ParentId);
            }
            return specification;
        }

        public ISpecification<Category> Build(Guid? categoryId)
        {
            return Specification<Category>.FromPredicate(category => category.Id == categoryId);
        }

        public ISpecification<Category> Build(string? name)
        {
            return Specification<Category>.FromPredicate(category => category.Name == name);
        }

    }
}
