using application.Customers.Commands;
using application.Customers.ViewModels;
using application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace application.Customers.Queries.GetAllCustomers;

public record class GetAllCustomers()
    : IRequest<CustomersListDto>;

public class Handler
    : IRequestHandler<GetAllCustomers, CustomersListDto>
{
    public Handler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public async Task<CustomersListDto> Handle(GetAllCustomers request, CancellationToken cancellationToken)
    {
        return new CustomersListDto(await _context.Customers
            .ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken));
    }
}


