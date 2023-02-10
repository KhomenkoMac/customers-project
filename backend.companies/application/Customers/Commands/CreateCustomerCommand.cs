using application.Exceptions;
using application.Interface;
using AutoMapper;
using domain;
using FluentValidation;
using MediatR;

namespace application.Customers.Commands;

public record class CreateCustomerCommand(
    string Name, 
    string Email, 
    string CompanyName, 
    string Phone)
    : IRequest;

public class Handler
    : IRequestHandler<CreateCustomerCommand>
{
    public Handler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var toCreate = _mapper.Map<Customer>(request);
            _context.Customers.Add(toCreate);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (InvalidOperationException)
        {
            throw new CustomerWithSameNameException();
        }
        catch
        {
            throw new Exception($"{nameof(CreateCustomerCommand)}: Enexcpected");
        }

        return Unit.Value;
    }
}

public class Validator : AbstractValidator<CreateCustomerCommand>
{
    public Validator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
        this.RuleFor(x => x.Email).Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        this.RuleFor(x => x.CompanyName).NotEmpty();
        this.RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}
