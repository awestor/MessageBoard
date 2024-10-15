using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.BasePagination;
using OrderBoard.Contracts.Categories.Requests;
using OrderBoard.Contracts.OrderItem.Requests;
using OrderBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Categories.SpecificationContext.Builders
{
    public interface ICategorySpecificationBuilder
    {
        /// <summary>
        /// Строит спецификацию по запросу.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Спецификация.</returns>
        ISpecification<Category> Build(SearchCategoryRequest request);


        ISpecification<Category> Build(Guid? categoryId);
        ISpecification<Category> Build(string? name);
    }
}
