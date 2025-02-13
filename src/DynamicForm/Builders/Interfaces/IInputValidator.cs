namespace DynamicForm.Interfaces
{
    public interface IInputValidator
    {
        InputValidator MaxLength(int max);
        InputValidator MinLength(int min);
        InputValidator OneOf(string[] options);
        InputValidator Required(bool isRequired = true);
    }
}