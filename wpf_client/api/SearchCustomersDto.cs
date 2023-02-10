namespace wpf_client.api;

public record class SearchCustomersQuery(
    string? Name,
    string? Email,
    string? CompanyName,
    string? Phone,
    int ItemsOnPage,
    int PageNumber);
