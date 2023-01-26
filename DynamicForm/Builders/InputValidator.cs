using DynamicForm.Interfaces;

namespace DynamicForm
{

    public abstract class InputValidator : IInputValidator, IBuilder, IContentSetter
    {
        private readonly Dictionary<string, object> _content = new();

        public InputValidator OneOf(string[] options)
        {
            _content["oneOf"] = options;
            return this;
        }

        public InputValidator MaxLength(int max)
        {
            _content["max"] = max;
            return this;
        }

        public InputValidator MinLength(int min)
        {
            _content["min"] = min;
            return this;
        }

        public InputValidator Required(bool isRequired = true)
        {
            _content["required"] = isRequired;
            return this;
        }

        public void Set(string key, object value)
        {
            _content[key] = value;
        }

        public Dictionary<string, object> Build()
        {
            return _content;
        }
    }
}