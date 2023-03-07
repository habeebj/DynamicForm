using System.Linq.Expressions;

namespace DynamicForm.Interfaces
{
    public interface IFormBuilder<TModel> where TModel : notnull
    {
        IInputBuilder<TModel, TProperty> TextField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression) where TProperty : notnull;
        IInputBuilder<TModel, TProperty> EmailField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression) where TProperty : notnull;
        IInputBuilder<TModel, TProperty> NumberField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression) where TProperty : notnull;
        IInputBuilder<TModel, TProperty> PasswordField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression) where TProperty : notnull;
        IInputBuilder<TModel, TProperty> TextAreaField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression) where TProperty : notnull;
        IInputBuilder<TModel, TProperty> Property<TProperty>(string propertyName, InputType inputType = InputType.Text) where TProperty : notnull;
        IInputBuilder<TModel, TProperty> ConfirmField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType type) where TProperty : notnull;
        IInputBuilder<TModel, TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType inputType = InputType.Text) where TProperty : notnull;

        IOptionBuilder<TModel, TProperty> CheckBox<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression) where TProperty : notnull;
        IOptionBuilder<TModel, TProperty> Select<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression) where TProperty : notnull;
        IFormInputBuilder<TModel, TProperty> FormInput<TProperty>(Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression) where TProperty : class;
    }
}