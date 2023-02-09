using DynamicForm.Interfaces;

namespace DynamicForm.Tests
{
    public class LoginFormConfiguration : FormConfiguration<User>
    {
        public override void Setup()
        {
            Index(0);
            Name("Login Form");
            Api(HttpMethod.Post, "/account");
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