using System.Linq.Expressions;
using DynamicForm.Builders.Interfaces;

namespace DynamicForm.Builders
{
    public class Comparer<TModel> : Interfaces.IComparer<TModel>
    {
        public Dictionary<string, object?> Equals<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty? value)
        {
            var field = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            return Build(field, Keys.EQUALS, value);
        }

        public Dictionary<string, object?> GreaterThan<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty? value)
        {
            var field = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            return Build(field, Keys.GREATER_THAN, value);
        }

        public Dictionary<string, object?> Contains<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty[] values)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object?> LessThan<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty? value)
        {
            var field = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            return Build(field, Keys.LESS_THAN, value);
        }

        public Dictionary<string, object?> NotEquals<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty? value)
        {
            var field = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            return Build(field, Keys.NOT_EQUALS, value);
        }

        public Dictionary<string, object?> NotContains<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty[] values)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, object?> Build(string? field, string @operator, object? value)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(field, "field");

            var dictionary = new Dictionary<string, object?>();
            dictionary[Keys.FIELD] = field;
            dictionary[Keys.OPERATOR] = @operator;
            dictionary[Keys.VALUE] = value;
            return dictionary;
        }
    }
}