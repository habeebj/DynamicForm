using System.Linq.Expressions;
using DynamicForm.Interfaces;

namespace DynamicForm
{
    public class InputBuilder<TProperty> : InputBuilder, IInputBuilder<TProperty>, IOptionBuilder<TProperty>
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
        public IInputBuilder<TProperty> WithUrl(string uriString, string property)
        {
            if (!Uri.IsWellFormedUriString(uriString, UriKind.RelativeOrAbsolute))
            {
                throw new ArgumentException("Invalid URI string");
            }

            ArgumentNullException.ThrowIfNullOrEmpty(property);
            return (InputBuilder<TProperty>)base.SetData(uriString, property.Split('.'));
        }

        public IInputBuilder<TProperty> WithUrl<TModel>(Uri uri, Expression<Func<TModel, IEnumerable<object>>> selectExpression)
        {
            var properties = new List<string>();
            var memberExpression = selectExpression.Body as MemberExpression;

            while (memberExpression != null)
            {
                properties.Add(memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return (IInputBuilder<TProperty>)base.SetData(uri.ToString(), properties);
        }

        public IInputBuilder<TProperty> AddOptions(IEnumerable<string> options)
            => (IInputBuilder<TProperty>)base.Options(options);

        public new IInputBuilder<TProperty> Label(string label)
            => (InputBuilder<TProperty>)base.Label(label);

        public new IInputBuilder<TProperty> Placeholder(string placeholder)
            => (InputBuilder<TProperty>)base.Placeholder(placeholder);

        public IInputBuilder<TProperty> WithValidation(Func<IInputValidator<TProperty>, IInputValidator<TProperty>> validation)
        {
            var validationContent = ((IBuilder)validation(new InputValidator<TProperty>())).Build();
            return (InputBuilder<TProperty>)base.Validation(validationContent);
        }
    }
}