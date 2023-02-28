using System.Linq.Expressions;
namespace DynamicForm.Interfaces
{
    public interface IFormInputBuilder<TModel, TProperty> where TProperty : notnull
    {
        IInputBuilder<TModel, TProperty> WithForm(Action<IFormBuilder<TProperty>> action, params Expression<Func<TProperty, object>>[] displayExpression);
    }
}