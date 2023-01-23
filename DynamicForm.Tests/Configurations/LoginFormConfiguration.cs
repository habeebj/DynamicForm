using DynamicForm.Interfaces;

namespace DynamicForm.Tests
{
    public class LoginFormConfiguration : IFormConfiguration<User>
    {
        public void Configure(IFormBuilder<User> builder)
        {
            builder
                .EmailField(x => x.Email)
                .Label("Email Address")
                .Placeholder("Enter Email address")
                .WithValidation(x => x.Required());

            builder
                .NumberField(x => x.Age)
                .Label(nameof(User.Age))
                .WithValidation(x => x.AllowNegative())
                .Placeholder("Enter your age");

            builder
                .PasswordField(x => x.Password)
                .Label("Password")
                .WithValidation(x => x.MinLength(10));

            builder
                .ConfirmField(x => x.Password, InputType.Password)
                .Label("Password");
        }
    }
}