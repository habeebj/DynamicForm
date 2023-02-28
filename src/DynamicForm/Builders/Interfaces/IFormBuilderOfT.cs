using System.Linq.Expressions;

namespace DynamicForm.Interfaces
{
    public interface IFormBuilder<TModel> where TModel : notnull
    {
        IInputBuilder<TModel, TProperty> TextField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TModel, TProperty> EmailField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TModel, TProperty> NumberField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TModel, TProperty> PasswordField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TModel, TProperty> TextAreaField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IInputBuilder<TModel, TProperty> Property<TProperty>(string propertyName, InputType inputType = InputType.Text);
        IInputBuilder<TModel, TProperty> ConfirmField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType type);
        IInputBuilder<TModel, TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType inputType = InputType.Text);

        IOptionBuilder<TModel, TProperty> CheckBox<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IOptionBuilder<TModel, TProperty> Select<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        IFormInputBuilder<TModel, TProperty> FormInput<TProperty>(Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression) where TProperty : class;
    }
}