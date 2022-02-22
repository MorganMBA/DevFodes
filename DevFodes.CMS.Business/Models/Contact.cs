using System.Text.Json.Serialization;

namespace DevFodes.CMS.Business.Models
{
    public class Contact
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
    }
}
