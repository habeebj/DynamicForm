using DynamicForm.Interfaces;

namespace DynamicForm
{
    public interface IFormConfiguration<TModel> where TModel : class
    {
        void Configure(IFormBuilder<TModel> builder);
    }
}