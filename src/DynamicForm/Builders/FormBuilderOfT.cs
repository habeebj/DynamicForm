using System.Linq.Expressions;
using DynamicForm.Interfaces;

namespace DynamicForm
{
    public class FormBuilder<TModel> : FormBuilder, IFormBuilder<TModel> where TModel : class
    {
        public IInputBuilder<TModel, TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType inputType = InputType.Text)
        {
            var propertyName = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            ArgumentNullException.ThrowIfNull(propertyName, nameof(propertyName));

            return Property<TProperty>(propertyName, inputType);
        }

        public IInputBuilder<TModel, TProperty> Property<TProperty>(string propertyName, InputType inputType = InputType.Text)
        {
            return base.Property<TModel, TProperty>(propertyName, inputType.ToString());
        }

        public IInputBuilder<TModel, TProperty> EmailField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Email);
        }

        public IInputBuilder<TModel, TProperty> PasswordField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Password);
        }

        public IInputBuilder<TModel, TProperty> NumberField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Number);
        }

        public IInputBuilder<TModel, TProperty> TextAreaField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.TextArea);
        }

        public IInputBuilder<TModel, TProperty> TextField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Property(propertyExpression, InputType.Text);
        }

        public IInputBuilder<TModel, TProperty> ConfirmField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType type)
        {
            var property = ((MemberExpression)propertyExpression.Body)?.Member.Name;
            ArgumentNullException.ThrowIfNull(property, nameof(property));

            var additionalFields = new Dictionary<string, object> { { "confirmField", property } };

            return Property<TModel, TProperty>($"Confirm{property}", type.ToString(), additionalFields);
        }

        public IOptionBuilder<TModel, TProperty> CheckBox<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return (IOptionBuilder<TModel, TProperty>)Property(propertyExpression, InputType.CheckBox);
        }

        public IOptionBuilder<TModel, TProperty> Select<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return (IOptionBuilder<TModel, TProperty>)Property(propertyExpression, InputType.Select);
        }
    }
}