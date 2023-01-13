namespace dynamic_form
{
    public interface IFormBuilder
    {
        // string Name { get; }
        // string Method { get; }
        // string URL { get; }
        // pre_request -> {index, key, data_key}
        IInputBuilder<TProperty> Property<TProperty>(string propertyName, string inputType, Dictionary<string, object>? additionalFields = null);
    }
}