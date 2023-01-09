using System.Linq.Expressions;

namespace dynamic_form;
public interface IOptionBuilder<TProperty>
{
    InputBuilder<TProperty> Data(string uri, string property);
    InputBuilder<TProperty> Options(IEnumerable<string> options);
    InputBuilder<TProperty> Data<TModel>(Uri uri, Expression<Func<TModel, object>> dataPathExpression);
}