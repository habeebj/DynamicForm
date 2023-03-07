namespace DynamicForm.Interfaces
{
    public interface IFormBuilder
    {
        IInputBuilder<TModel, TProperty> Property<TModel, TProperty>(string propertyName, string inputType, Dictionary<string, object>? additionalAttributes = null) where TProperty : notnull;
    }
}