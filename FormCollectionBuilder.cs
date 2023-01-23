using System.Reflection;
namespace dynamic_form
{
    public class FormCollectionBuilder : IFormCollectionBuilder, IBuilder
    {
        private readonly Dictionary<string, object> _content = new();
        private readonly IList<FormBuilder> _formBuilders = new List<FormBuilder>();

        public virtual FormCollectionBuilder ApplyConfiguration<TEntity>(IFormConfiguration<TEntity> configuration) where TEntity : class
        {
            var builder = new FormBuilder<TEntity>();
            configuration.Configure(builder);
            _formBuilders.Add(builder);
            return this;
        }

        public virtual FormCollectionBuilder ApplyConfiguration(Assembly assembly, Func<Type, bool>? predicate = null)
        {
            var applyEntityConfigurationMethod = typeof(FormCollectionBuilder)
                .GetMethods()
                .Single(
                    t => t.Name == nameof(ApplyConfiguration)
                    && t.ContainsGenericParameters
                    && t.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition() == typeof(IFormConfiguration<>)
                );

            foreach (var type in assembly.GetTypes().OrderBy(t => t.FullName))
            {
                // Only accept types that contain a parameterless constructor, are not abstract and satisfy a predicate if it was used.
                if (type.GetConstructor(Type.EmptyTypes) == null || (!predicate?.Invoke(type) ?? false))
                {
                    continue;
                }

                foreach (var @interface in type.GetInterfaces())
                {
                    if (!@interface.IsGenericType)
                    {
                        continue;
                    }

                    if (@interface.GetGenericTypeDefinition() == typeof(IFormConfiguration<>))
                    {
                        var target = applyEntityConfigurationMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
                        target.Invoke(this, new[] { Activator.CreateInstance(type) });
                    }
                }
            }

            return this;
        }

        public Dictionary<string, object> Build()
        {
            _content["data"] = _formBuilders.Select(x => x.Build());
            return _content;
        }
    }
}