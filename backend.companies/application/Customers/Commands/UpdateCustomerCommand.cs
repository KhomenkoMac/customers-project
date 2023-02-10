using application.Exceptions;
using application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace application.Customers.Commands.UpdateCustomerCommand;

public record class UpdateCustomerCommand(
    string Name,
    string Email,
    string CompanyName,
    string Phone)
    : IRequest;

public class Handler
    : IRequestHandler<UpdateCustomerCommand>
{
    public Handler(IAppDbContext context)
    {
        _context = context;
    }
    
    private readonly IAppDbContext _context;

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var toUpdate = await _context
                .Customers
                .SingleAsync(x => x.Name == request.Name, cancellationToken);

            toUpdate.Email = request.Email;
            toUpdate.CompanyName = request.CompanyName;
            toUpdate.Phone = request.Phone;

            _context
                .Customers
                .Update(toUpdate);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (ArgumentException)
        {
            throw new CustomerNotFoundException();
        }
        catch
        {
            throw new Exception($"{nameof(UpdateCustomerCommand)}: Enexcpected");
        }

        return Unit.Value;
    }
}
