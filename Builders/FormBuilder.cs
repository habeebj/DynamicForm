using System.Linq.Expressions;

namespace dynamic_form
{
    public class FormBuilder : IFormBuilder, IBuilder
    {
        protected readonly Dictionary<string, object> _content = new Dictionary<string, object>();
        protected readonly IList<InputBuilder> _inputs = new List<InputBuilder>();

        // public string Build(ISerializer serializer)
        // {
        //     _content["form"] = _inputs.Select(x => ((IContentBuilder)x).Content);
        //     return serializer.Serialize(_content);
        // }

        public Dictionary<string, object> Build()
        {
            _content["form"] = _inputs.Select(x => x.Build());
            return _content;
        }

        public IInputBuilder<TProperty> Property<TProperty>(string propertyName, string inputType, Dictionary<string, object>? additionalFields = null)
        {
            var inputBuilder = new InputBuilder<TProperty>(propertyName, inputType);

            if (additionalFields != null)
            {
                foreach (var item in additionalFields)
                {
                    ((IContentSetter)inputBuilder).Set(item.Key, item.Value);
                }
            }

            _inputs.Add(inputBuilder);

            return inputBuilder;
        }
    }

    public class FormBuilder<TModel> : FormBuilder, IFormBuilder<TModel> where TModel : class
    {
        public IInputBuilder<TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType inputType = InputType.Text)
        {
            var propertyName = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            ArgumentNullException.ThrowIfNull(propertyName, nameof(propertyName));

            return Property<TProperty>(propertyName, inputType);
        }

        public IInputBuilder<TProperty> Property<TProperty>(string propertyName, InputType inputType = InputType.Text)
        {
            return base.Property<TProperty>(propertyName, inputType.ToString());
        }

        public IInputBuilder<TProperty> EmailField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Email);
        }

        public IInputBuilder<TProperty> PasswordField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Password);
        }

        public IInputBuilder<TProperty> NumberField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Number);
        }

        public IInputBuilder<TProperty> TextAreaField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.TextArea);
        }

        public IInputBuilder<TProperty> TextField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Text);
        }

        public IInputBuilder<TProperty> ConfirmField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType type)
        {
            var property = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            ArgumentNullException.ThrowIfNull(property, nameof(property));

            var additionalFields = new Dictionary<string, object> { { "confirmField", property } };

            return Property<TProperty>($"Confirm{property}", type.ToString(), additionalFields);
        }

        public IOptionBuilder<TProperty> CheckBox<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return (IOptionBuilder<TProperty>)Property(propertyExpression, InputType.CheckBox);
        }

        public IOptionBuilder<TProperty> Select<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return (IOptionBuilder<TProperty>)Property(propertyExpression, InputType.Select);
        }
    }
}