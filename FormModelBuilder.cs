using System.Reflection;
namespace dynamic_form
{
    public interface IFormModelBuilder
    {
        FormModelBuilder ApplyConfiguration<TEntity>(IFormConfiguration<TEntity> configuration) where TEntity : class;
        FormModelBuilder ApplyConfigurationsFromAssembly(Assembly assembly, Func<Type, bool>? predicate = null);
    }

    public class FormModelBuilder : IFormModelBuilder, IBuilder
    {
        private readonly Dictionary<string, object> _content = new();
        private readonly IList<FormBuilder> _formBuilders = new List<FormBuilder>();

        public virtual FormModelBuilder ApplyConfiguration<TEntity>(IFormConfiguration<TEntity> configuration) where TEntity : class
        {
            var builder = new FormBuilder<TEntity>();
            configuration.Configure(builder);
            _formBuilders.Add(builder);
            return this;
        }

        public virtual FormModelBuilder ApplyConfigurationsFromAssembly(Assembly assembly, Func<Type, bool>? predicate = null)
        {
            // TODO: Scan through assembly for configuration
            return this;
        }

        public string Build(ISerializer serializer)
        {
            _content["data"] = _formBuilders.Select(x => ((IContentBuilder)x).Content);
            return serializer.Serialize(_content);
        }
    }

    // public class DynamicFormBuilder
    // {
    //     // FormModelBuilder
    //     // builder
    //     public void OnFormCreate(ModelBuilder builder)
    //     {
    //         builder.ApplyConfigurationsFromAssembly(typeof(DynamicFormBuilder).Assembly);
    //     }
    // }
}