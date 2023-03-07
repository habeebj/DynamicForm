using System.Linq.Expressions;
using DynamicForm.Interfaces;
using DynamicForm.Utilities;

namespace DynamicForm
{
    public class InputBuilder<TModel, TProperty> : InputBuilder, IInputBuilder<TModel, TProperty>, IOptionBuilder<TModel, TProperty>, IFormInputBuilder<TModel, TProperty> where TProperty : notnull
    {
        private static string[] GetRecursiveProperties(LambdaExpression expression)
        {
            var properties = new List<string>();
            var memberExpression = GetMemberExpression(expression);
            while (memberExpression != null)
            {
                var propertyName = memberExpression?.Member.Name;
                if (!string.IsNullOrEmpty(propertyName))
                {
                    properties.Add(propertyName);
                }
                memberExpression = memberExpression?.Expression as MemberExpression;
            }

            return properties.ToArray();
        }

        private static MemberExpression? GetMemberExpression(LambdaExpression expression)
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

        private static string[] GetProperties(LambdaExpression[]? expressions)
        {
            var properties = new List<string>();
            expressions = expressions ?? new LambdaExpression[] { };
            foreach (var expression in expressions)
            {
                var propertyName = GetMemberExpression(expression)?.Member?.Name;
                if (!string.IsNullOrEmpty(propertyName))
                {
                    properties.Add(propertyName);
                }
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
        public IInputBuilder<TModel, TProperty> WithUrl(string uriString, string? property = null, string selectKey = "name")
        {
            if (!Uri.IsWellFormedUriString(uriString, UriKind.RelativeOrAbsolute))
            {
                throw new ArgumentException("Invalid URI string");
            }

            return (InputBuilder<TModel, TProperty>)base.SetData(uriString, property?.Split('.') ?? new string[] { }, selectKey);
        }

        public IInputBuilder<TModel, TProperty> WithUrl<TResponseModel, TSelectKeyModel>(Uri uri, Expression<Func<TResponseModel, IEnumerable<object>>> selectExpression,
            Expression<Func<TSelectKeyModel, object>>? selectKey = null)
        {
            var properties = GetRecursiveProperties(selectExpression);
            var selectKeyProperty = (selectKey?.Body as MemberExpression)?.Member.Name;
            return (IInputBuilder<TModel, TProperty>)base.SetData(uri.ToString(), properties, selectKeyProperty);
        }

        public IInputBuilder<TModel, TProperty> DependsOn(params Expression<Func<TModel, object>>[] propertyExpressions)
        {
            var properties = GetProperties(propertyExpressions);
            return (IInputBuilder<TModel, TProperty>)base.DependsOn(properties);
        }

        public IInputBuilder<TModel, TProperty> AddOptions(IEnumerable<Option> options)
            => (IInputBuilder<TModel, TProperty>)base.Options(options);

        public new IInputBuilder<TModel, TProperty> Label(string label)
            => (InputBuilder<TModel, TProperty>)base.Label(label);

        public new IInputBuilder<TModel, TProperty> Disabled() => (InputBuilder<TModel, TProperty>)base.Disabled();

        public IInputBuilder<TModel, TProperty> Lookup<TResponse>(Expression<Func<TModel, TProperty>> idExpression, Expression<Func<TResponse, TProperty>> keyExpression)
        {
            string idProperty = (idExpression.Body as MemberExpression)!.Member.Name;
            string keyProperty = (keyExpression.Body as MemberExpression)!.Member.Name;
            return (InputBuilder<TModel, TProperty>)base.Lookup(idProperty, keyProperty);
        }

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
            var properties = GetRecursiveProperties(dataAccessor);
            return (InputBuilder<TModel, TProperty>)base.RemoteValidation(method.ToString(), url, properties);
        }

        public IInputBuilder<TModel, TProperty> WithForm(Action<IFormBuilder<TProperty>> action, params Expression<Func<TProperty, object>>[] displayExpression)
        {
            var formBuilder = new FormBuilder<TProperty>();
            action.Invoke(formBuilder);
            var properties = GetProperties(displayExpression);
            return (InputBuilder<TModel, TProperty>)base.Form(formBuilder.Build(), properties);
        }

        public IOptionBuilder<TModel, TProperty> WithCreateForm<T>(IFormConfiguration<T> formConfiguration) where T : class
        {
            var builder = new FormBuilder<T>();
            formConfiguration.Configure(builder);
            return (InputBuilder<TModel, TProperty>)base.AddForm(builder.Build());
        }
    }
}