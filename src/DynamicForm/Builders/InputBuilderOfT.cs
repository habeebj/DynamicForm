using System.Linq.Expressions;
using DynamicForm.Interfaces;
using DynamicForm.Utilities;

namespace DynamicForm
{
    public class InputBuilder<TModel, TProperty> : InputBuilder, IInputBuilder<TModel, TProperty>, IOptionBuilder<TModel, TProperty>
    {
        public InputBuilder(string id, string type)
        {
            base.Type(type.ToLower());
            base.Id(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriString"></param>
        /// <param name="property">dot separated example: responseObject.results</param>
        /// <returns></returns>
        public IInputBuilder<TModel, TProperty> WithUrl(string uriString, string property)
        {
            if (!Uri.IsWellFormedUriString(uriString, UriKind.RelativeOrAbsolute))
            {
                throw new ArgumentException("Invalid URI string");
            }

            ArgumentNullException.ThrowIfNullOrEmpty(property);
            return (InputBuilder<TModel, TProperty>)base.SetData(uriString, property.Split('.'));
        }

        public IInputBuilder<TModel, TProperty> WithUrl<TResponseModel>(Uri uri, Expression<Func<TResponseModel, IEnumerable<object>>> selectExpression)
        {
            var memberExpression = selectExpression.Body as MemberExpression;
            var properties = GetProperties(memberExpression);
            return (IInputBuilder<TModel, TProperty>)base.SetData(uri.ToString(), properties);
        }

        private static string[] GetProperties(MemberExpression? memberExpression)
        {
            var properties = new List<string>();
            while (memberExpression != null)
            {
                properties.Add(memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return properties.ToArray();
        }

        public IInputBuilder<TModel, TProperty> DependsOn(params Expression<Func<TModel, TProperty>>[] propertyExpressions)
        {
            var properties = new List<string>();
            foreach (var expression in propertyExpressions)
            {
                var propertyName = ((MemberExpression)expression.Body).Member.Name;
                properties.Add(propertyName);
            }

            return (IInputBuilder<TModel, TProperty>)base.DependsOn(properties.ToArray());
        }

        public IInputBuilder<TModel, TProperty> AddOptions(IEnumerable<string> options)
            => (IInputBuilder<TModel, TProperty>)base.Options(options);

        public new IInputBuilder<TModel, TProperty> Label(string label)
            => (InputBuilder<TModel, TProperty>)base.Label(label);

        public new IInputBuilder<TModel, TProperty> Placeholder(string placeholder)
            => (InputBuilder<TModel, TProperty>)base.Placeholder(placeholder);

        public IInputBuilder<TModel, TProperty> WithValidation(Func<IInputValidator<TProperty>, IInputValidator<TProperty>> validation)
        {
            var validationContent = ((IBuilder)validation(new InputValidator<TProperty>())).Build();
            var validationType = ValidationTypeConverter.Convert<TProperty>();
            return (InputBuilder<TModel, TProperty>)base.Validation(validationType, validationContent);
        }

        public IInputBuilder<TModel, TProperty> RemoteValidation<TResponseModel>(HttpMethod method, string url, Expression<Func<TResponseModel, object>> dataAccessor)
        {
            var memberExpression = dataAccessor.Body as MemberExpression;
            var properties = GetProperties(memberExpression);

            return (InputBuilder<TModel, TProperty>)base.RemoteValidation(method.ToString(), url, properties, null);
        }
    }
}