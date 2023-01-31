using DynamicForm.Interfaces;

namespace DynamicForm
{

    public abstract class InputValidator : IInputValidator, IBuilder, IContentSetter
    {
        private readonly Dictionary<string, object> _content = new();

        public InputValidator OneOf(string[] options)
        {
            _content[Keys.ONE_OF] = options;
            return this;
        }

        public InputValidator MaxLength(int max)
        {
            _content[Keys.MAX] = max;
            return this;
        }

        public InputValidator MinLength(int min)
        {
            _content[Keys.MIN] = min;
            return this;
        }

        public InputValidator Required(bool isRequired = true)
        {
            _content[Keys.REQUIRED] = isRequired;
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