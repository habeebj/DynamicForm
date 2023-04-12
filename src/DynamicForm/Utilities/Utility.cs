using System.Linq.Expressions;
using System.Reflection;
using DynamicForm.Options;

namespace DynamicForm.Utilities
{
    public static class Utility
    {
        public static string[] GetRecursiveProperties(LambdaExpression? expression)
        {
            if (expression is null)
            {
                return new string[] { };
            }

            var properties = new List<string>();

            var memberExpression = GetMemberExpression(expression);
            while (memberExpression != null)
            {
                var propertyName = GetPropertyNameFromMemberInfo(memberExpression?.Member);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    properties.Add(propertyName);
                }
                memberExpression = memberExpression?.Expression as MemberExpression;
            }

            return properties.ToArray();
        }

        public static MemberExpression? GetMemberExpression(LambdaExpression expression)
        {
            MemberExpression? memberExpression = null;
            switch (expression.Body)
            {
                case MemberExpression:
                    memberExpression = (expression.Body as MemberExpression);
                    break;
                case UnaryExpression:
                    memberExpression = ((expression.Body as UnaryExpression)?.Operand as MemberExpression);
                    break;
                default:
                    break;
            }

            return memberExpression;
        }

        public static string? GetPropertyNameFromString(string? name)
        {
            return PropertyOptions.UseCamelCasingForPropertyName ? name?.ToCamelCase() : name;
        }

        public static string? GetPropertyNameFromMemberInfo(MemberInfo? memberInfo)
        {
            return GetPropertyNameFromString(memberInfo?.Name);
        }

        public static string? GetPropertyNameFromExpression(Expression? expression)
        {
            if (expression is MemberExpression memberExpression)
            {
                return GetPropertyNameFromMemberInfo(memberExpression?.Member);
            }

            return null;
        }

        public static string[] GetProperties(LambdaExpression[]? expressions)
        {
            var properties = new List<string>();
            expressions = expressions ?? new LambdaExpression[] { };
            foreach (var expression in expressions)
            {
                var propertyName = GetPropertyNameFromExpression(GetMemberExpression(expression));
                if (!string.IsNullOrEmpty(propertyName))
                {
                    properties.Add(propertyName);
                }
            }

            return properties.ToArray();
        }
    }
}