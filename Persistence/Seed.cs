using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Users.Any())
                return;

            var users = new List<User>
            {
                new User
                {
                    Name = "John",
                    Surname = "Doe",
                    Balance = 5000m
                },
                new User
                {
                    Name = "Jane",
                    Surname = "Doe",
                    Balance = 6000m
                },
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
