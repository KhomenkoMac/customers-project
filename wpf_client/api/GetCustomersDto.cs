using System.Text.Json.Serialization;

namespace wpf_client.api;

public class GetCustomersDto
{
    [JsonPropertyName("customers")]
    public CustomerDto[] Customers { get; set; }
}
