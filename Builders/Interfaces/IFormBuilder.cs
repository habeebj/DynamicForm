namespace dynamic_form
{
    public interface IFormBuilder
    {
        IInputBuilder<TProperty> Property<TProperty>(string propertyName, string inputType, Dictionary<string, object>? additionalFields = null);
    }
}