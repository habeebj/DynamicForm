using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicForm.AspCore")]
namespace DynamicForm.Options
{
    internal static class PropertyOptions
    {
        internal static bool UseCamelCasingForPropertyName = true;
    }
}