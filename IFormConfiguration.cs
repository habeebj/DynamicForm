namespace dynamic_form
{
    public interface IFormConfiguration<TModel> where TModel : class
    {
        void Configure(IFormBuilder<TModel> builder);
    }
}