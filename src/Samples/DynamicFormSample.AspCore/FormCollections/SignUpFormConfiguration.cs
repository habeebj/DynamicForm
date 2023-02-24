using DynamicForm;
using DynamicForm.Interfaces;
using DynamicFormSample.AspCore.Models;

namespace DynamicFormSample.AspCore.FormCollections
{
    public class SignUpFormConfiguration : FormConfiguration<User>, IMerchantOnboarding
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
                .WithValidation(x => x.MinLength(10).Required());

            builder
                .ConfirmField(x => x.Password, InputType.Password)
                .Label("Confirm Password")
                .WithValidation(x => x.Required());
        }

        public override void Setup()
        {
            Index(0);
            Api(HttpMethod.Post, "/accounts/sign-up");
        }
    }
}