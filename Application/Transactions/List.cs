using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Transaction>>
        {
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Transaction>>
        {
            public readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Transaction>> Handle(
                Query request,
                CancellationToken cancellationToken
            )
            {
                return await _context
                    .Transactions.Where(x =>
                        x.Sender == request.UserId || x.Receiver == request.UserId
                    )
                    .ToListAsync();
            }
        }
    }
}
