using System.Linq.Expressions;

namespace dynamic_form;
public interface IOptionBuilder<TProperty>
{
    IInputBuilder<TProperty> Data(string uri, string property);
    IInputBuilder<TProperty> Options(IEnumerable<string> options);
    IInputBuilder<TProperty> Data<TModel>(Uri uri, Expression<Func<TModel, IEnumerable<object>>> selectExpression);
}