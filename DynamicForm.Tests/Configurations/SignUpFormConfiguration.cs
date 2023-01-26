using DynamicForm.Interfaces;

namespace DynamicForm.Tests
{
    public class SignUpFormConfiguration : IFormConfiguration<User>
    {
        public void Configure(IFormBuilder<User> builder)
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