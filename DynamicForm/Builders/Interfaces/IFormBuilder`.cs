using System.Linq.Expressions;

namespace DynamicForm.Interfaces
{
    public interface IFormBuilder<TModel> where TModel : class
    {
        IInputBuilder<TProperty> TextField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TProperty> EmailField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TProperty> NumberField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TProperty> PasswordField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TProperty> TextAreaField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);

        IInputBuilder<TProperty> Property<TProperty>(string propertyName, InputType inputType = InputType.Text);
        IInputBuilder<TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType inputType = InputType.Text);

        IInputBuilder<TProperty> ConfirmField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType type);

        IOptionBuilder<TProperty> CheckBox<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IOptionBuilder<TProperty> Select<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
    }
}