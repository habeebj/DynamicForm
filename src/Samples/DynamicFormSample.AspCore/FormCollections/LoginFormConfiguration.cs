using DynamicForm;
using DynamicForm.Interfaces;
using DynamicFormSample.AspCore.Models;

namespace DynamicFormSample.AspCore.FormCollections
{
    public class LoginFormConfiguration : FormConfiguration<User>
    {
        public override void OnConfigure(IFormBuilder<User> builder)
        {
            builder
                .EmailField(x => x.Email)
                .Label("Email Address")
                .Placeholder("Enter Email address")
                .WithValidation(x => x.Required());

            builder
                .PasswordField(x => x.Password)
                .Label("Password")
                .WithValidation(x => x.MinLength(10));
        }

        public override void Setup()
        {
            Api(HttpMethod.Post, "/accounts");
        }
    }
}