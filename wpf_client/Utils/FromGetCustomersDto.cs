using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using wpf_client.api;
using wpf_client.ViewModels;

namespace wpf_client.Utils;

public static class GetCustomersDtoExt
{
    public static List<CustomerViewModel> FromDto(this GetCustomersDto dto)
    {
        return dto
            .Customers
            .Select(x => new CustomerViewModel
            {
                Name = x.Name,
                CompanyName = x.CompanyName,
                Email = x.Email,
                Phone = x.Phone,
            })
            .ToList();
    }

    public static CustomerDto ToDto(this CustomerViewModel vms)
    {
        return new CustomerDto
        {
            Name = vms.Name,
            CompanyName = vms.CompanyName,
            Email = vms.Email,
            Phone = vms.Phone,
        };
    }
}
