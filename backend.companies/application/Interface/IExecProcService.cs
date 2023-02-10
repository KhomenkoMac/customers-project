using application.Customers.Queries.SearchAndPageCustomersQuery;
using domain;

namespace application.Interface;

public interface IExecProcService
{
    IList<Customer> Execute(SearchAndPageCustomersQuery queryParams);
}
