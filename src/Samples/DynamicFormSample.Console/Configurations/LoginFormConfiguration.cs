using DynamicForm;
using DynamicForm.Interfaces;

namespace Sample.Configurations
{
    public class LoginFormConfiguration : FormConfiguration<User>
    {
        public override void Setup()
        {
            Name("Login Form");
            Api(HttpMethod.Post, "/accounts");
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

            builder
                .PasswordField(x => x.Password)
                .DependsOn(x => x.Email, x => x.Name)
                .Label("Confirm Password");
        }
    }
}