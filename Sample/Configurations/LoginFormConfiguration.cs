using DynamicForm;
using DynamicForm.Interfaces;

namespace Sample.Configurations
{
    public class LoginFormConfiguration : FormConfiguration<User>
    {
        public override void Setup()
        {
            
            Name("Login Form");
            Api(HttpMethod.Post, "https://webhook.site/8fd12daf-2e7c-403c-9431-50319f18f08f");
        }

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
    }
}