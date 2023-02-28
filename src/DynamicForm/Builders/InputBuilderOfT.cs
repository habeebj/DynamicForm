using System.Linq.Expressions;
using DynamicForm.Interfaces;
using DynamicForm.Utilities;

namespace DynamicForm
{
    public class InputBuilder<TModel, TProperty> : InputBuilder, IInputBuilder<TModel, TProperty>, IOptionBuilder<TModel, TProperty>, IFormInputBuilder<TModel, TProperty> where TProperty : notnull
    {
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

        private static string[] GetProperties(MemberExpression[]? memberExpressions)
        {
            var properties = new List<string>();
            memberExpressions = memberExpressions ?? new MemberExpression[] { };

            foreach (var expression in memberExpressions)
            {
                var propertyName = expression.Member.Name;
                properties.Add(propertyName);
            }

            return properties.ToArray();
        }

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

        public IInputBuilder<TModel, TProperty> DependsOn(params Expression<Func<TModel, TProperty>>[] propertyExpressions)
        {
            var memberExpressions = propertyExpressions.Select(x => ((MemberExpression)x.Body));
            var properties = GetProperties(memberExpressions?.ToArray());
            return (IInputBuilder<TModel, TProperty>)base.DependsOn(properties);
        }

        public IInputBuilder<TModel, TProperty> AddOptions(IEnumerable<Option> options)
            => (IInputBuilder<TModel, TProperty>)base.Options(options);

        public new IInputBuilder<TModel, TProperty> Label(string label)
            => (InputBuilder<TModel, TProperty>)base.Label(label);

        public new IInputBuilder<TModel, TProperty> Disabled() => (InputBuilder<TModel, TProperty>)base.Disabled();

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

        public IInputBuilder<TModel, TProperty> WithForm(Action<IFormBuilder<TProperty>> action, params Expression<Func<TProperty, object>>[] displayExpression)
        {
            var formBuilder = new FormBuilder<TProperty>();
            action.Invoke(formBuilder);
            var memberExpressions = displayExpression.Select(x => ((MemberExpression)x.Body));
            var properties = GetProperties(memberExpressions?.ToArray());
            return (InputBuilder<TModel, TProperty>)base.Form(formBuilder.Build(), properties);
        }
    }
}