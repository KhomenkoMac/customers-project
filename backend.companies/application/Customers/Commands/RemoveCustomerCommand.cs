using application.Exceptions;
using application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace application.Customers.Commands.RemoveCustomerCommand;

public record class RemoveCustomerCommand(string Name)
    : IRequest;

public class Handler
    : IRequestHandler<RemoveCustomerCommand>
{
    public Handler(IAppDbContext context)
    {
        _context = context;
    }

    private readonly IAppDbContext _context;

    public async Task<Unit> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var toRemove = await _context
            .Customers
            .SingleAsync(x => x.Name == request.Name);

            _context.Customers.Remove(toRemove);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (ArgumentNullException)
        {
            throw new CustomerNotFoundException();
        }
        catch
        {
            throw new Exception($"{nameof(RemoveCustomerCommand)}: Enexcpected");
        }

        return Unit.Value;
    }
}
