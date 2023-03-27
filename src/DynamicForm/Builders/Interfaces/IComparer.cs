using System.Linq.Expressions;

namespace DynamicForm.Builders.Interfaces
{
    public interface IComparer<TModel>
    {
        Dictionary<string, object?> Equals<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty? value);
        Dictionary<string, object?> LessThan<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty? value);
        Dictionary<string, object?> NotEquals<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty? value);
        Dictionary<string, object?> Contains<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty[] values);
        Dictionary<string, object?> GreaterThan<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty? value);
        Dictionary<string, object?> NotContains<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty[] values);
    }
}