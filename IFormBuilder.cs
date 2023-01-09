using System.Linq.Expressions;

namespace dynamic_form
{
    public interface IFormBuilder<TModel> where TModel : class
    {
        InputBuilder<TProperty> TextField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        InputBuilder<TProperty> EmailField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        InputBuilder<TProperty> NumberField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        InputBuilder<TProperty> PasswordField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        InputBuilder<TProperty> TextAreaField<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
        InputBuilder<TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, InputType inputType = InputType.Text);

        IOptionBuilder<TProperty> CheckBox<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);
    }
}