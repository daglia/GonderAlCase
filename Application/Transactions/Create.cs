using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Transaction Transaction { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var senderUser = _context.Users.FirstOrDefault(x =>
                    x.Id == request.Transaction.Sender
                );
                var receiverUser = _context.Users.FirstOrDefault(x =>
                    x.Id == request.Transaction.Receiver
                );

                if (senderUser is null || receiverUser is null)
                    return;

                senderUser.Balance -= request.Transaction.Amount;
                receiverUser.Balance += request.Transaction.Amount;

                _context.Transactions.Add(request.Transaction);

                await _context.SaveChangesAsync();
            }
        }
    }
}
