using System.Text.Json.Serialization;

namespace wpf_client.api;

public class CustomerDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}
