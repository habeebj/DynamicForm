using System.Linq.Expressions;

namespace DynamicForm.Interfaces;
public interface IOptionBuilder<TModel, TProperty>
{
    IInputBuilder<TModel, TProperty> WithUrl(string uriString, string? property = null, string selectKey = "name");
    IInputBuilder<TModel, TProperty> AddOptions(IEnumerable<Option> options);
    IInputBuilder<TModel, TProperty> WithUrl<TResponseModel, TSelectKeyModel>(Uri uri, Expression<Func<TResponseModel, IEnumerable<object>>> selectExpression, Expression<Func<TSelectKeyModel, object>>? selectKey = null);
}