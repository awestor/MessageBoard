using FluentValidation;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Items.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Other.Validators.Items
{
    public class SearchItemByNameValidator : AbstractValidator<SearchItemByNameRequest>
    {
        public SearchItemByNameValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(3).WithMessage("Имя должно быть не менее 3 символов");
            RuleFor(x => x.MinPrice).NotNull().GreaterThan(-1).WithMessage("Некорректная цена");
            RuleFor(x => x.MaxPrice).NotNull().GreaterThan(0).WithMessage("Некорректная цена");
            RuleFor(x => x.Skip).NotNull().GreaterThan(-1);
            RuleFor(x => x.Take).NotNull().GreaterThan(0);
            return;
        }
    }
}
