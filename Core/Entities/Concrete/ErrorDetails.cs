using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.Entities.Concrete
{
    public class ErrorDetails
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public bool Success => false;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
