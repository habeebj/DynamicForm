using DynamicForm.Interfaces;

namespace DynamicForm
{
    public abstract class FormBuilder : IFormBuilder, IBuilder
    {
        protected readonly Dictionary<string, object> _content = new();
        protected readonly IList<InputBuilder> _inputs = new List<InputBuilder>();

        // public abstract string URL { get; set; }
        // public abstract string Name { get; set; }
        // public abstract string Method { get; set; }

        public Dictionary<string, object> Build()
        {
            _content["form"] = _inputs.Select(x => x.Build());
            return _content;
        }

        public IInputBuilder<TProperty> Property<TProperty>(string propertyName, string inputType, Dictionary<string, object>? additionalAttributes = default)
        {
            var inputBuilder = new InputBuilder<TProperty>(propertyName, inputType);

            if (additionalAttributes != null)
            {
                foreach (var attribute in additionalAttributes)
                {
                    ((IContentSetter)inputBuilder).Set(attribute.Key, attribute.Value);
                }
            }

            _inputs.Add(inputBuilder);

            return inputBuilder;
        }
    }
}