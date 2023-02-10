using application.Customers.ViewModels;
using application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace application.Customers.Queries.SearchAndPageCustomersQuery;

public record class SearchAndPageCustomersQuery(
    string? Name,
    string? Email,
    string? CompanyName,
    string? Phone,
    int ItemsOnPage = 5,
    int PageNumber = 1)
    : IRequest<CustomersListDto>;


public class Handler : IRequestHandler<SearchAndPageCustomersQuery, CustomersListDto>
{
    public Handler(IExecProcService procedureExecutor)
    {
        _procedureExecutor = procedureExecutor;
    }

    private readonly IExecProcService _procedureExecutor;

    public Task<CustomersListDto> Handle(SearchAndPageCustomersQuery request, CancellationToken cancellationToken)
    {
        var result = _procedureExecutor
            .Execute(request)
            .Select(x => new CustomerViewModel(x.Name, x.Email, x.CompanyName, x.Phone))
            .ToArray();
        return Task.FromResult(new CustomersListDto(result));
    }
}