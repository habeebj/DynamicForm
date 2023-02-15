using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using DynamicForm.Interfaces;

namespace DynamicForm
{
    internal sealed record Api(string method, string url);

    public record PreRequest(int index, string key, string data_Key);

    public abstract class FormConfiguration<TModel> : IFormConfiguration<TModel> where TModel : class
    {
        private Api? _api;
        private int _index;
        private string _name;
        private List<PreRequest> _preRequests = new();

        protected FormConfiguration()
        {
            _name = this.GetType().Name.Replace("Configuration", string.Empty);
        }

        protected void Name(string name) => _name = name;

        protected void Index(int index) => _index = index;

        protected void PreRequest<TEntity, TProperty>(int index, Expression<Func<TEntity, TProperty>> dataKeyExpression, Expression<Func<TModel, TProperty>> keyExpression) where TEntity : class
        {
            var dataKeyProperty = ((MemberExpression)dataKeyExpression.Body)?.Member.Name;
            ArgumentNullException.ThrowIfNull(dataKeyProperty, nameof(dataKeyProperty));

            var keyProperty = ((MemberExpression)keyExpression.Body)?.Member.Name;
            ArgumentNullException.ThrowIfNull(keyProperty, nameof(keyProperty));

            _preRequests.Add(new PreRequest(index, keyProperty, dataKeyProperty));
        }

        protected void Api(HttpMethod method, Uri url) => _api = new Api(method.Method, url.ToString());

        protected void Api(HttpMethod method, [StringSyntax(StringSyntaxAttribute.Uri)] string urlString)
        {
            Api(method, new Uri(urlString, UriKind.RelativeOrAbsolute));
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
                _builder.Set(Keys.PRE_REQUEST, _preRequests);
                _builder.Set(Keys.API, _api ?? new object());
            }
        }
    }
}