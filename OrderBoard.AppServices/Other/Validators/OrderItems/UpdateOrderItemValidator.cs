using FluentValidation;
using OrderBoard.Contracts.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Other.Validators.OrderItems
{
    public class UpdateOrderItemValidator : AbstractValidator<OrderItemUpdateModel>
    {
        public UpdateOrderItemValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Count).NotNull().GreaterThan(0);
            return;
        }
    }
}
