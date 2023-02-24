using System.Linq.Expressions;

namespace DynamicForm.Interfaces
{
    public interface IInputBuilder<TModel, TProperty>
    {
        IInputBuilder<TModel, TProperty> Label(string label);

        IInputBuilder<TModel, TProperty> Placeholder(string placeholder);

        IInputBuilder<TModel, TProperty> DependsOn(params Expression<Func<TModel, TProperty>>[] propertyExpressions);

        IInputBuilder<TModel, TProperty> WithValidation(Func<IInputValidator<TProperty>, IInputValidator<TProperty>> validation);
    }

    // public interface IPropertyBuilder<TProperty>
    // {
    //     IPropertyBuilder<TProperty> WithVisuals(Action<IPropertyVisual<TProperty>> validation);
    //     IPropertyBuilder<TProperty> WithValidation(Action<IPropertyValidator<TProperty>> validation);
    //     IPropertyBuilder<TProperty> WithOptions(Action<ISelectProperty<TProperty>> options);
    // }
}