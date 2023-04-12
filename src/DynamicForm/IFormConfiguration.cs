using DynamicForm.Interfaces;

namespace DynamicForm
{
    public interface IFormConfiguration<TModel> where TModel : class
    {
        void Setup();
        void Configure(IFormBuilder<TModel> builder);
    }
}