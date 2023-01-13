using System.Linq.Expressions;
namespace dynamic_form
{

    public class InputBuilder<TProperty> : InputBuilder, IInputBuilder<TProperty>, IOptionBuilder<TProperty>
    {
        public InputBuilder(string property, string type)
        {
            base.Type(type.ToLower());
            base.Id(property);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="property">dot separated example: responseObject.results</param>
        /// <returns></returns>
        public IInputBuilder<TProperty> Data(string uri, string property)
        {
            // TODO: validate URI
            ArgumentNullException.ThrowIfNullOrEmpty(property);
            return (InputBuilder<TProperty>)base.Data(uri, property.Split('.'));
        }

        public IInputBuilder<TProperty> Data<TModel>(Uri uri, Expression<Func<TModel, IEnumerable<object>>> selectExpression)
        {
            var properties = new List<string>();
            var memberExpression = selectExpression.Body as MemberExpression;

            while (memberExpression != null)
            {
                properties.Add(memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return (IInputBuilder<TProperty>)base.Data(uri.ToString(), properties);
        }

        public new IInputBuilder<TProperty> Options(IEnumerable<string> options)
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