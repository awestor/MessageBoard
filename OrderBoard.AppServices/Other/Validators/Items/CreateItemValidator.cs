using FluentValidation;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.Contracts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Other.Validators.Items
{
    public class CreateItemValidator : AbstractValidator<ItemCreateModel>
    {
        public CreateItemValidator()
        { 
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.Count).NotNull().GreaterThan(0);
            RuleFor(x => x.CategoryId).NotNull().NotEqual(Guid.Empty);
            return;
        }
    }
}
