using DynamicForm.Interfaces;

namespace DynamicForm
{
    public class InputValidator<TProperty> : InputValidator, IInputValidator<TProperty>
    {
        public new IInputValidator<TProperty> MinLength(int min) => (InputValidator<TProperty>)base.MinLength(min);
        public new IInputValidator<TProperty> MaxLength(int max) => (InputValidator<TProperty>)base.MaxLength(max);

        public new IInputValidator<TProperty> OneOf(string[] options) => (InputValidator<TProperty>)base.OneOf(options);
        public new IInputValidator<TProperty> Required(bool isRequired = true) => (InputValidator<TProperty>)base.Required(isRequired);
    }
}