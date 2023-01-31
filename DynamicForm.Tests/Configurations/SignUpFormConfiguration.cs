using DynamicForm.Interfaces;

namespace DynamicForm.Tests
{
    public class SignUpFormConfiguration : FormConfiguration<User>
    {
        public override void Setup()
        {
            Index(2);
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
                .ConfirmField(x => x.Password, InputType.Password)
                .Label("Confirm Password");
        }
    }
}