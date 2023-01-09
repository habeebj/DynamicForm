using System.Linq.Expressions;

namespace dynamic_form
{
    public class FormBuilder<TModel> : IBuilder, IFormBuilder<TModel> where TModel : class
    {
        private readonly Dictionary<string, object> _content = new Dictionary<string, object>();
        private readonly IList<InputBuilder> _inputs = new List<InputBuilder>();

        public string Build(ISerializer serializer)
        {
            _content["form"] = _inputs.Select(x => ((IContentBuilder)x).Content);
            return serializer.Serialize(_content);
        }

        public InputBuilder<TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType inputType = InputType.Text)
        {
            var type = Enum.GetName<InputType>(inputType);
            ArgumentNullException.ThrowIfNullOrEmpty(type, nameof(type));

            var property = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            ArgumentNullException.ThrowIfNull(property, nameof(property));

            var inputBuilder = new InputBuilder<TProperty>(property, type);
            _inputs.Add(inputBuilder);

            return inputBuilder;
        }

        public InputBuilder<TProperty> EmailField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Email);
        }

        public InputBuilder<TProperty> PasswordField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Password);
        }

        public InputBuilder<TProperty> NumberField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Number);
        }

        public InputBuilder<TProperty> TextAreaField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.TextArea);
        }

        public InputBuilder<TProperty> TextField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Text);
        }

        public IOptionBuilder<TProperty> CheckBox<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return (OptionBuilder<TProperty>)Property(propertyExpression, InputType.Text);
        }
    }
}