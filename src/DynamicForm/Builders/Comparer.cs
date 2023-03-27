using System.Linq.Expressions;

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
            var field = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            return Build(field, Keys.CONTAINS, values);
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
            var field = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            return Build(field, Keys.NOT_CONTAIN, values);
        }

        private Dictionary<string, object?> Build(string? field, string @operator, object? value)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(field, Keys.FIELD);

            var dictionary = new Dictionary<string, object?>();
            dictionary[Keys.FIELD] = field;
            dictionary[Keys.OPERATOR] = @operator;
            dictionary[Keys.VALUE] = value;
            return dictionary;
        }
    }
}