using System.Text.Json.Serialization;

namespace wpf_client.api;

public class DeleteCustomerCommand
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
