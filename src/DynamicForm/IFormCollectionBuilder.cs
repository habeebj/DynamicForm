using System.Reflection;
namespace DynamicForm
{
    public interface IFormCollectionBuilder
    {
        FormCollectionBuilder ApplyConfiguration<TEntity>(IFormConfiguration<TEntity> configuration) where TEntity : class;
        FormCollectionBuilder ApplyConfiguration(Assembly assembly, Func<Type, bool>? predicate = null);
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