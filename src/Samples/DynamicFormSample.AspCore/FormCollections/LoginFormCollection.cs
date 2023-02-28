using DynamicForm;

namespace DynamicFormSample.AspCore.FormCollections
{
    public class LoginFormCollection : DynamicForm.FormCollection
    {
        protected override void OnFormCreating(FormCollectionBuilder formBuilder)
        {
            formBuilder.ApplyConfiguration(new LoginFormConfiguration());
        }

        protected override void Setup()
        {
            Name("user Login form");
            BaseUrl("http://api.google.com");
            Description("user login form");
        }
    }
}