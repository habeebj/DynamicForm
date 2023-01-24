using System.Linq.Expressions;

namespace DynamicForm.Interfaces;
public interface IOptionBuilder<TProperty>
{
    IInputBuilder<TProperty> WithUrl(string uri, string property);
    IInputBuilder<TProperty> AddOptions(IEnumerable<string> options);
    IInputBuilder<TProperty> WithUrl<TModel>(Uri uri, Expression<Func<TModel, IEnumerable<object>>> selectExpression);
}