using System.Linq.Expressions;

namespace DynamicForm.Interfaces;
public interface IOptionBuilder<TModel, TProperty>
{
    IInputBuilder<TModel, TProperty> WithUrl(string uriString, string property);
    IInputBuilder<TModel, TProperty> AddOptions(IEnumerable<string> options);
    IInputBuilder<TModel, TProperty> WithUrl<TResponseModel>(Uri uri, Expression<Func<TResponseModel, IEnumerable<object>>> selectExpression);
}