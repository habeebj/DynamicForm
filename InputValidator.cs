namespace dynamic_form
{
    public interface IContentSetter
    {
        void Set(string key, object value);
    }

    public class InputValidator : IInputValidator, IContentBuilder, IContentSetter
    {
        private readonly Dictionary<string, object> _content = new();

        public Dictionary<string, object> Content => _content;

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
    }
}