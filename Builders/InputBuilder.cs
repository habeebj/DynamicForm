namespace dynamic_form
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
            _content["id"] = id;
            return this;
        }

        protected InputBuilder Type(string type)
        {
            _content["type"] = type;
            return this;
        }

        protected InputBuilder Label(string label)
        {
            _content["label"] = label;
            return this;
        }

        protected InputBuilder Options(IEnumerable<string> options)
        {
            _content["options"] = options;
            return this;
        }

        protected InputBuilder SetData(string uri, IEnumerable<string> dataPath)
        {
            _content["pullUrl"] = uri;
            _content["selectDataAccessor"] = dataPath;
            return this;
        }

        protected InputBuilder Placeholder(string placeHolder)
        {
            _content["placeholder"] = placeHolder;
            return this;
        }

        public void Set(string key, object value)
        {
            _content[key] = value;
        }

        protected InputBuilder Validation(Dictionary<string, object> validations)
        {
            const string validationKey = "validation";
            var validationContent = new Dictionary<string, object>();
            if (_content.TryGetValue(validationKey, out var validationContentObject))
            {
                validationContent = validationContentObject as Dictionary<string, object>;
            }

            ArgumentNullException.ThrowIfNull(validationContent);

            foreach (var validation in validations)
            {
                validationContent[validation.Key] = validation.Value;
            }

            _content[validationKey] = validationContent;

            return this;
        }
    }
}