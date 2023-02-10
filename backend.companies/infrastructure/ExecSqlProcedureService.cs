using application.Customers.Queries.SearchAndPageCustomersQuery;
using application.Interface;
using domain;
using Microsoft.EntityFrameworkCore;

namespace infrastructure;

public class ExecSqlProcedureService : IExecProcService
{
    public ExecSqlProcedureService(IAppDbContext context)
    {
        _context = context;
    }

    private readonly IAppDbContext _context;

    public IList<Customer> Execute(SearchAndPageCustomersQuery queryParams)
    {
        var result = _context
            .Customers
            // is sql-injection safe
            .FromSqlInterpolated($"exec SearchCustomers @pageNumber={queryParams.PageNumber}, @itemsOnPage={queryParams.ItemsOnPage}, @name={queryParams.Name}, @email={queryParams.Email}, @companyName={queryParams.CompanyName}, @phone={queryParams.Phone}")
            .ToList();

        return result;
    }
}
