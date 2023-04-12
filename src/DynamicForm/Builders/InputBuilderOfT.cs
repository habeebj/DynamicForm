using System.Linq.Expressions;
using DynamicForm.Interfaces;
using DynamicForm.Utilities;

namespace DynamicForm
{
    public class InputBuilder<TModel, TProperty> : InputBuilder, IInputBuilder<TModel, TProperty>, IOptionBuilder<TModel, TProperty>, IFormInputBuilder<TModel, TProperty> where TProperty : notnull
    {
        public InputBuilder(string id, string type)
        {
            base.Type(type.ToLower());
            base.Id(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriString"></param>
        /// <param name="property">dot separated example: responseObject.results</param>
        /// <returns></returns>
        public IInputBuilder<TModel, TProperty> WithUrl(string uriString, string? property = null, string selectKey = "name")
        {
            if (!Uri.IsWellFormedUriString(uriString, UriKind.RelativeOrAbsolute))
            {
                throw new ArgumentException("Invalid URI string");
            }

            return (InputBuilder<TModel, TProperty>)base.SetData(uriString, property?.Split('.') ?? new string[] { }, selectKey);
        }

        public IInputBuilder<TModel, TProperty> WithUrl<TResponseModel, TSelectKeyModel>(Uri uri, Expression<Func<TResponseModel, IEnumerable<object>>>? dataPathExpression = null, Expression<Func<TSelectKeyModel, object>>? selectKey = null)
        {
            var properties = Utility.GetRecursiveProperties(dataPathExpression);
            var selectKeyProperty = Utility.GetPropertyNameFromExpression((selectKey?.Body));
            return (IInputBuilder<TModel, TProperty>)base.SetData(uri.ToString(), properties, selectKeyProperty);
        }

        public IInputBuilder<TModel, TProperty> DependsOn(params Expression<Func<TModel, object>>[] propertyExpressions)
        {
            var properties = Utility.GetProperties(propertyExpressions);
            return (IInputBuilder<TModel, TProperty>)base.DependsOn(properties);
        }

        public IInputBuilder<TModel, TProperty> AddOptions(IEnumerable<Option> options)
            => (IInputBuilder<TModel, TProperty>)base.Options(options);

        public new IInputBuilder<TModel, TProperty> Label(string label)
            => (InputBuilder<TModel, TProperty>)base.Label(label);

        public new IInputBuilder<TModel, TProperty> Disabled() => (InputBuilder<TModel, TProperty>)base.Disabled();

        public IInputBuilder<TModel, TProperty> Lookup<TResponse>(Expression<Func<TModel, TProperty>> idExpression, Expression<Func<TResponse, TProperty>> keyExpression)
        {
            string idProperty = Utility.GetPropertyNameFromExpression(idExpression.Body)!;
            string keyProperty = Utility.GetPropertyNameFromExpression(keyExpression.Body)!;

            return (InputBuilder<TModel, TProperty>)base.Lookup(idProperty, keyProperty);
        }

        public new IInputBuilder<TModel, TProperty> Placeholder(string placeholder)
            => (InputBuilder<TModel, TProperty>)base.Placeholder(placeholder);

        public IInputBuilder<TModel, TProperty> WithValidation(Func<IInputValidator<TProperty>, IInputValidator<TProperty>> validation)
        {
            var validationContent = ((IBuilder)validation(new InputValidator<TProperty>())).Build();
            var validationType = ValidationTypeConverter.Convert<TProperty>();
            return (InputBuilder<TModel, TProperty>)base.Validation(validationType, validationContent);
        }

        public IInputBuilder<TModel, TProperty> RemoteValidation<TResponseModel>(HttpMethod method, string url, Expression<Func<TResponseModel, object>> dataAccessor)
        {
            // TODO: URL validation
            var properties = Utility.GetRecursiveProperties(dataAccessor);
            return (InputBuilder<TModel, TProperty>)base.RemoteValidation(method.ToString(), url, properties);
        }

        public IInputBuilder<TModel, TProperty> WithForm(Action<IFormBuilder<TProperty>> action, params Expression<Func<TProperty, object>>[] displayExpression)
        {
            var formBuilder = new FormBuilder<TProperty>();
            action.Invoke(formBuilder);
            var properties = Utility.GetProperties(displayExpression);
            return (InputBuilder<TModel, TProperty>)base.Form(formBuilder.Build(), properties);
        }

        public IOptionBuilder<TModel, TProperty> WithCreateForm<T>(IFormConfiguration<T> formConfiguration) where T : class
        {
            var builder = new FormBuilder<T>();
            formConfiguration.Configure(builder);
            return (InputBuilder<TModel, TProperty>)base.AddForm(builder.Build());
        }

        public IInputBuilder<TModel, TProperty> VisibleOn(Func<Builders.Interfaces.IComparer<TModel>, Dictionary<string, object?>> comparerBuilder)
        {
            var content = comparerBuilder.Invoke(new Builders.Comparer<TModel>());
            return (InputBuilder<TModel, TProperty>)base.VisibleOn(content);
        }

        public IInputBuilder<TModel, TProperty> HiddenOn(Func<Builders.Interfaces.IComparer<TModel>, Dictionary<string, object?>> comparerBuilder)
        {
            var content = comparerBuilder.Invoke(new Builders.Comparer<TModel>());
            return (InputBuilder<TModel, TProperty>)base.HiddenOn(content);
        }
    }
}