namespace dynamic_form
{
    public class InputBuilder : IContentBuilder
    {
        private readonly Dictionary<string, object> _content;

        public Dictionary<string, object> Content => _content;

        protected InputBuilder()
        {
            _content = new Dictionary<string, object>();
        }

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

        protected InputBuilder Placeholder(string placeHolder)
        {
            _content["placeholder"] = placeHolder;
            return this;
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