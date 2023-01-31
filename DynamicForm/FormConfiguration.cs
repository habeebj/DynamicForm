using DynamicForm.Interfaces;

namespace DynamicForm
{
    internal sealed record Api(string method, string urlString);

    public record PreRequest();

    public abstract class FormConfiguration<TModel> : IFormConfiguration<TModel> where TModel : class
    {
        private Api? _api;
        private int _index;
        private string _name;
        private IEnumerable<PreRequest>? _preRequests;

        protected FormConfiguration()
        {
            _name = this.GetType().Name.Replace("Configuration", string.Empty);
        }

        protected void Name(string name) => _name = name;

        protected void Index(int index) => _index = index;

        protected void Api(HttpMethod method, Uri url) => _api = new Api(method.Method, url.ToString());

        protected void Api(HttpMethod method, string urlString)
        {
            Api(method, new Uri(urlString, UriKind.RelativeOrAbsolute));
        }

        protected void PreRequest(IEnumerable<PreRequest> preRequests)
        {
            _preRequests = preRequests;
        }

        public abstract void Setup();

        public abstract void OnConfigure(IFormBuilder<TModel> builder);

        public void Configure(IFormBuilder<TModel> builder)
        {
            Setup();
            OnConfigure(builder);
            if (builder is FormBuilder _builder)
            {
                _builder.Set(Keys.NAME, _name);
                _builder.Set(Keys.INDEX, _index);
                _builder.Set(Keys.API, _api ?? new object());
            }
        }
    }
}