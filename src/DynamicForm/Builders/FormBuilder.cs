using DynamicForm.Interfaces;

namespace DynamicForm
{
    public abstract class FormBuilder : IFormBuilder, IBuilder, IContentSetter
    {
        protected readonly Dictionary<string, object> _content = new();
        protected readonly IList<InputBuilder> _inputs = new List<InputBuilder>();

        public Dictionary<string, object> Build()
        {
            _content[Keys.FORM] = _inputs.Select(x => x.Build());
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

        public void Set(string key, object value)
        {
            _content[key] = value;
        }
    }
}