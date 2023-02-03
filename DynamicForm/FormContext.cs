using DynamicForm.Interfaces;

namespace DynamicForm
{
    public abstract class FormContext : IBuilder
    {
        private string _name;
        private string? _baseUrl;
        private string? _description;
        private readonly FormCollectionBuilder _formCollectionBuilder = new();

        protected FormContext()
        {
            _name = this.GetType().Name.Replace("Context", string.Empty);
        }

        protected void Name(string name) => _name = name;
        protected void BaseUrl(string url) => _baseUrl = url;
        protected void BaseUrl(Uri uri) => _baseUrl = uri.ToString();
        protected void Description(string description) => _description = description;

        protected abstract void Setup();
        protected abstract void OnFormCreating(FormCollectionBuilder formBuilder);

        public Dictionary<string, object> Build()
        {
            Setup();
            _formCollectionBuilder.Set(Keys.NAME, _name);
            _formCollectionBuilder.Set(Keys.BASE_URL, _baseUrl ?? string.Empty);
            _formCollectionBuilder.Set(Keys.DESCRIPTION, _description ?? string.Empty);
            OnFormCreating(_formCollectionBuilder);
            return _formCollectionBuilder.Build();
        }
    }
}