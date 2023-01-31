namespace DynamicForm
{
    public interface IBuilderContext
    {
        void OnFormModelCreating(FormCollectionBuilder formBuilder);
    }

    public class BuilderContext : IBuilderContext
    {
        public virtual void OnFormModelCreating(FormCollectionBuilder formBuilder)
        {
        }
    }
}