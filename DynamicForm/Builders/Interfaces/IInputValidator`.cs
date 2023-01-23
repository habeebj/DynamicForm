namespace DynamicForm.Interfaces
{
    public interface IInputValidator<TProperty>
    {
        IInputValidator<TProperty> MinLength(int min);
        IInputValidator<TProperty> MaxLength(int max);
        IInputValidator<TProperty> OneOf(string[] options);
        IInputValidator<TProperty> Required(bool isRequired = true);
    }
}