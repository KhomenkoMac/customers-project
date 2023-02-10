using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using System.Xml.Linq;
using wpf_client.api;
using wpf_client.Utils;

namespace wpf_client.ViewModels;

public partial class CustomerViewModel : ObservableObject
{
    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private string? companyName;

    [ObservableProperty]
    private string? email;

    [ObservableProperty]
    private string? phone;

    public bool IsTemplate { get; set; }
}

public partial class CustomersViewModel : ObservableObject
{
    public CustomersViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
        LoadCustomersCommand = new AsyncRelayCommand(LoadCustomers);
        AddCustomerCommand = new RelayCommand(AddNewCustomerTemplate);
        SaveCustomerCommand = new AsyncRelayCommand(SaveCustomer);
        RemoveCustomerCommand = new AsyncRelayCommand(RemoveCustomer);
        SearchByCriteriaCommand = new AsyncRelayCommand(SearchByCriteria);
        ClearForQueringCommand = new RelayCommand(ClearForQuering);
    }

    private readonly HttpClient _httpClient;


    [ObservableProperty]
    private bool nameFieldCanBeUsed;

    [ObservableProperty]
    private CustomerViewModel? currentCustomer;

    public ObservableCollection<CustomerViewModel> Customers { get; set; } = new();

    public IAsyncRelayCommand LoadCustomersCommand { get; }

    public IRelayCommand AddCustomerCommand { get; }
    public IAsyncRelayCommand SaveCustomerCommand { get; }
    public IAsyncRelayCommand RemoveCustomerCommand { get; }
    public IAsyncRelayCommand SearchByCriteriaCommand { get; }
    public IRelayCommand ClearForQueringCommand { get; }

    private async Task LoadCustomers()
    {
        var result = await _httpClient
            .GetFromJsonAsync<GetCustomersDto>
            ("https://localhost:7299/api/Customers/GetAll");
        var asVM = result?.FromDto();
        
        if (Customers.Any())
        {
            Customers.Clear();
        }
        
        asVM!.ForEach(x=> Customers.Add(x));
    }

    private void AddNewCustomerTemplate()
    {
        var template = new CustomerViewModel
        {
            Name = "New Customer Name",
            Email = "example@gmail.com",
            CompanyName = "New Customer Company Name",
            Phone = "448889863",
            IsTemplate= true,
        };
        Customers.Add(template);
        CurrentCustomer = template;
    }
    
    private async Task SaveCustomer()
    {
        if (CurrentCustomer == null)
        {
            return;
        }

        var json = JsonSerializer.Serialize(CurrentCustomer.ToDto());

        if (CurrentCustomer!.IsTemplate)
        {
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7299/api/Customers/Post", requestContent);
        }
        else
        {
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7299/api/Customers/Update", requestContent);
        }
    }

    private async Task SearchByCriteria()
    {
        if (currentCustomer == null)
        {
            return;
        }
        var uri = $"https://localhost:7299/api/Customers/GetBy?";

        var query = HttpUtility.ParseQueryString(string.Empty);
        
        query["name"] = currentCustomer!.Name;
        query["email"] = currentCustomer!.Email;
        query["companyName"] = currentCustomer!.CompanyName;
        query["phone"] = CurrentCustomer!.Phone;

        Customers.Clear();

        var result = await _httpClient
            .GetFromJsonAsync<GetCustomersDto>(uri + query.ToString());

        result!
            .FromDto()
            .ForEach(x => Customers.Add(x));
    }

    private void ClearForQuering()
    {
        CurrentCustomer = new CustomerViewModel
        {
            Name = "QueryParam1",
            Email = "",
            CompanyName = "",
            Phone = "",
        };
    }

    private async Task RemoveCustomer()
    {
        if (currentCustomer == null) { return; }

        var result = await _httpClient.DeleteAsync($"https://localhost:7299/api/Customers/Delete/{currentCustomer.Name}");

        Customers.Remove(currentCustomer!);
    }

}
