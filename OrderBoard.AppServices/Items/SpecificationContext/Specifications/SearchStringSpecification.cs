using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Domain.Entities;
using System.Linq.Expressions;


namespace OrderBoard.AppServices.Items.SpecificationContext.Specifications;

/// <summary>
/// Спецификация поиска объявлений по поисковой строке.
/// </summary>
public class SearchStringSpecification : Specification<Item>
{
    private readonly string _searchString;

    /// <summary>
    /// Создаёт спецификацию поиска объявлений по поисковой строке.
    /// </summary>
    /// <param name="searchString">Поисковая строка.</param>
    public SearchStringSpecification(string searchString)
    {
        _searchString = searchString;
    }

    /// <inheritdoc />
    public override Expression<Func<Item, bool>> PredicateExpression =>
        item =>
            item.Name.ToLower().Contains(_searchString.ToLower()) ||
            item.Description.ToLower().Contains(_searchString.ToLower());
}