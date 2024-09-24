using MessageBoard.Domain.Entities;

namespace MessageBoard.DataAccess.FakeDB
{
    internal class Users
    {
        public static IEnumerable<User> Get()
        {
            var result = new List<User>()
            {
                new()
                {
                    id = Guid.Parse("BCC6AB95-FEDA-4A80-9F77-64D0D092C0DE"),
                    Login = "Login1",
                    Description = "Description",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    id = Guid.Parse("9D18C43B-D769-481B-8D0D-5738F958963A"),
                    Login = "Login1",
                    Description = "Description",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    id = Guid.Parse("1D4EFC1B-C40D-4414-9BCC-9911C144745B"),
                    Login = "Login1",
                    Description = "Description",
                    CreatedAt = DateTime.UtcNow
                },
            };
            return result;
        }
    }
}
