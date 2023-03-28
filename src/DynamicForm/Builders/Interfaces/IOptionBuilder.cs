using System.Linq.Expressions;

namespace DynamicForm.Interfaces;
public interface IOptionBuilder<TModel, TProperty> where TProperty : notnull
{
    IInputBuilder<TModel, TProperty> AddOptions(IEnumerable<Option> options);
    IOptionBuilder<TModel, TProperty> WithCreateForm<T>(IFormConfiguration<T> formConfiguration) where T : class;
    IInputBuilder<TModel, TProperty> WithUrl(string uriString, string? property = null, string selectKey = "name");
    IInputBuilder<TModel, TProperty> WithUrl<TResponseModel, TSelectKeyModel>(Uri uri, Expression<Func<TResponseModel, IEnumerable<object>>>? dataPathExpression = null, Expression<Func<TSelectKeyModel, object>>? selectKey = null);
}