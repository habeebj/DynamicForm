namespace DynamicForm.Utilities
{
    public static class ValidationTypeConverter
    {
        public static string Convert<TProperty>() => typeof(TProperty).Name switch
        {
            "String" => "string",
            "Guid" => "string",
            "DateTime" => "date",
            "Boolean" => "boolean",
            "Int32" => "number",
            "Double" => "number",
            "Decimal" => "number",
            _ => "string"
        };
    }
}