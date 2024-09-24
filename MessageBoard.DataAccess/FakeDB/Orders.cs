using MessageBoard.Contracts.Enums;
using MessageBoard.Domain.Entities;

namespace MessageBoard.DataAccess.FakeDB
{
    public static class Orders
    {
        public static IEnumerable<Order> Get()
        {
            var result = new List<Order>()
            {
                new()
                {
                    id = Guid.Parse("413BCB53-7838-42F0-91BF-B2C8B2C62C73"),
                    TotalPrice = 10m,
                    TotalCount = 200,
                    Description = "Description -> 413BCB53-7838-42F0-91BF-B2C8B2C62C73",
                    CreatedAt = DateTime.UtcNow,
                    UserId = Guid.Parse("BCC6AB95-FEDA-4A80-9F77-64D0D092C0DE"),
                    OrderStatus = OrderStatus.Draft
                },
                new()
                {
                    id = Guid.Parse("2E4ACC6A-B2E8-444D-83B2-CCFABFC97306"),
                    TotalPrice = 10m,
                    TotalCount = 200,
                    Description = "Description -> 2E4ACC6A-B2E8-444D-83B2-CCFABFC97306",
                    CreatedAt = DateTime.UtcNow,
                    UserId = Guid.Parse("9D18C43B-D769-481B-8D0D-5738F958963A"),
                    OrderStatus = OrderStatus.Draft
                },
                new()
                {
                    id = Guid.Parse("E3BDF9E6-3054-48F6-8AAC-07144C596221"),
                    TotalPrice = 10m,
                    TotalCount = 200,
                    Description = "Description -> E3BDF9E6-3054-48F6-8AAC-07144C596221",
                    CreatedAt = DateTime.UtcNow,
                    UserId = Guid.Parse("9D18C43B-D769-481B-8D0D-5738F958963A"),
                    OrderStatus = OrderStatus.Draft
                },
            };
            return result;
        }
    }
}
