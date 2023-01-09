using System.Text.Json;

namespace dynamic_form
{
    // serializer -> json, yaml
    public class Serializer : ISerializer
    {
        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}