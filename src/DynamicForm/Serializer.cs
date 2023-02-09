using System.Text.Json;

namespace DynamicForm
{
    public class Serializer
    {
        public static string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}