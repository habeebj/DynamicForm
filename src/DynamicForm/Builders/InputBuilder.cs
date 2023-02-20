using DynamicForm.Interfaces;

namespace DynamicForm
{
    public abstract class InputBuilder : IContentSetter, IBuilder
    {
        private readonly Dictionary<string, object> _content;

        protected InputBuilder()
        {
            _content = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Build() => _content;

        protected InputBuilder Id(string id)
        {
            _content[Keys.ID] = id;
            return this;
        }

        protected InputBuilder Type(string type)
        {
            _content[Keys.TYPE] = type;
            return this;
        }

        protected InputBuilder Label(string label)
        {
            _content[Keys.LABEL] = label;
            return this;
        }

        protected InputBuilder Options(IEnumerable<string> options)
        {
            _content[Keys.OPTIONS] = options;
            return this;
        }

        protected InputBuilder SetData(string uri, IEnumerable<string> dataPath)
        {
            _content[Keys.PULL_URL] = uri;
            _content[Keys.SELECT_DATA_ACCESSOR] = dataPath;
            return this;
        }

        protected InputBuilder Placeholder(string placeHolder)
        {
            _content[Keys.PLACEHOLDER] = placeHolder;
            return this;
        }

        public void Set(string key, object value)
        {
            _content[key] = value;
        }

        protected InputBuilder Validation(string validationType, Dictionary<string, object> validations)
        {
            var validationContent = new Dictionary<string, object>();
            if (_content.TryGetValue(Keys.VALIDATION, out var validationContentObject))
            {
                validationContent = validationContentObject as Dictionary<string, object>;
            }

            ArgumentNullException.ThrowIfNull(validationContent);

            foreach (var validation in validations)
            {
                validationContent[validation.Key] = validation.Value;
            }

            if (!string.IsNullOrEmpty(validationType))
            {
                validationContent[Keys.TYPE] = validationType.ToLower();
            }

            _content[Keys.VALIDATION] = validationContent;

            return this;
        }
    }
}