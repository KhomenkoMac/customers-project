using application.Customers.Commands;
using application.Customers.ViewModels;
using AutoMapper;
using domain;

namespace application.Customers.Mapping;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer,CustomerViewModel>();
        CreateMap<CustomerViewModel, Customer>();
        CreateMap<CreateCustomerCommand, Customer>();
    }
}
