namespace dynamic_form
{
    public class InputBuilder<TProperty> : InputBuilder
    {
        public InputBuilder(string property, string type)
        {
            base.Type(type.ToLower());
            base.Id(property);
        }

        public new InputBuilder<TProperty> Label(string label) => (InputBuilder<TProperty>)base.Label(label);

        public new InputBuilder<TProperty> Placeholder(string placeholder) => (InputBuilder<TProperty>)base.Placeholder(placeholder);
        
        public InputBuilder<TProperty> WithValidation(Func<IInputValidator<TProperty>, IInputValidator<TProperty>> validation)
        {
            var validationContent = ((IContentBuilder)validation(new InputValidator<TProperty>())).Content;
            return (InputBuilder<TProperty>)base.Validation(validationContent);
        }
    }

    // public interface IPropertyBuilder<TProperty>
    // {
    //     IPropertyBuilder<TProperty> WithVisuals(Action<IPropertyVisual<TProperty>> validation);
    //     IPropertyBuilder<TProperty> WithValidation(Action<IPropertyValidator<TProperty>> validation);
    //     IPropertyBuilder<TProperty> WithOptions(Action<ISelectProperty<TProperty>> options);
    // }
}