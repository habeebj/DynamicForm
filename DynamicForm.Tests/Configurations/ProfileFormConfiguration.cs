using DynamicForm.Interfaces;

namespace DynamicForm.Tests.Configurations
{
    public interface IScannable { }

    public class ProfileFormConfiguration : IFormConfiguration<User>, IScannable
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
                .TextAreaField(x => x.Biography)
                .Label(nameof(User.Biography))
                .WithValidation(x => x.MaxLength(100))
                .Placeholder("Biography");
        }
    }
}