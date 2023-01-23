namespace DynamicForm.Interfaces
{
    public interface IInputBuilder<TProperty>
    {
        IInputBuilder<TProperty> Label(string label);

        IInputBuilder<TProperty> Placeholder(string placeholder);

        IInputBuilder<TProperty> WithValidation(Func<IInputValidator<TProperty>, IInputValidator<TProperty>> validation);
    }

    // public interface IPropertyBuilder<TProperty>
    // {
    //     IPropertyBuilder<TProperty> WithVisuals(Action<IPropertyVisual<TProperty>> validation);
    //     IPropertyBuilder<TProperty> WithValidation(Action<IPropertyValidator<TProperty>> validation);
    //     IPropertyBuilder<TProperty> WithOptions(Action<ISelectProperty<TProperty>> options);
    // }
}