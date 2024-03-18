using Application.Activities;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Tests
{
    public class CreateTests
    {
        private readonly DataContext _context;

        public CreateTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            SeedData(_context).Wait();
        }

        private async Task SeedData(DataContext context)
        {
            if (!context.Users.Any())
            {
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

        [Fact]
        public async Task Handle_ValidTransaction_Success()
        {
            var userList = await _context.Users.ToListAsync();
            var senderId = userList[0].Id;
            var receiverId = userList[1].Id;
            var amount = 100.0m;

            var command = new Create.Command
            {
                Transaction = new Transaction
                {
                    Sender = senderId,
                    Receiver = receiverId,
                    Amount = amount
                }
            };

            var handler = new Create.Handler(_context);

            await handler.Handle(command, CancellationToken.None);

            var senderUser = await _context.Users.FindAsync(senderId);
            var receiverUser = await _context.Users.FindAsync(receiverId);

            Assert.NotNull(senderUser);
            Assert.NotNull(receiverUser);
            Assert.Equal(4900.0m, senderUser.Balance);
            Assert.Equal(6100.0m, receiverUser.Balance);
        }
    }
}
