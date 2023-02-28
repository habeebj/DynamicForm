using System.Linq.Expressions;

namespace DynamicForm.Interfaces
{
    public interface IInputBuilder<TModel, TProperty>
    {
        IInputBuilder<TModel, TProperty> Disabled();
        IInputBuilder<TModel, TProperty> Disabled<TResponse>(Expression<Func<TModel, TProperty>> idExpression, Expression<Func<TResponse, TProperty>> keyExpression);
        IInputBuilder<TModel, TProperty> Label(string label);

        IInputBuilder<TModel, TProperty> Placeholder(string placeholder);

        IInputBuilder<TModel, TProperty> DependsOn(params Expression<Func<TModel, TProperty>>[] propertyExpressions);

        IInputBuilder<TModel, TProperty> WithValidation(Func<IInputValidator<TProperty>, IInputValidator<TProperty>> validation);
        IInputBuilder<TModel, TProperty> RemoteValidation<TResponseModel>(HttpMethod method, string url, Expression<Func<TResponseModel, object>> dataAccessor);
    }

    // public interface IPropertyBuilder<TProperty>
    // {
    //     IPropertyBuilder<TProperty> WithVisuals(Action<IPropertyVisual<TProperty>> validation);
    //     IPropertyBuilder<TProperty> WithValidation(Action<IPropertyValidator<TProperty>> validation);
    //     IPropertyBuilder<TProperty> WithOptions(Action<ISelectProperty<TProperty>> options);
    // }
}