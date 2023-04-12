using DynamicForm;
using DynamicForm.Interfaces;

namespace Sample.Configurations
{
    public class LoginFormConfiguration : FormConfiguration<User>
    {
        public override void Setup()
        {
            Index(0);
            Name("Login Form");
            Api(HttpMethod.Post, "/accounts");
        }

        public override void OnConfigure(IFormBuilder<User> builder)
        {
            builder
                .EmailField(x => x.Email)
                .Label("Email Address")
                .Placeholder("Enter Email address")
                .DependsOn(x => x.Name)
                .RemoteValidation<Person>(HttpMethod.Get, "http://api.co/?email{Email}", x => x.Name)
                .WithValidation(x => x.Required());

            builder
                .PasswordField(x => x.Password)
                .Label("Password")
                .WithValidation(x => x.MinLength(10))
                .VisibleOn(x => x.NotEquals(x => x.Email, null));

            builder
                .ConfirmField(x => x.Password, InputType.Password)
                .Label("Confirm Password")
                .DependsOn(x => x.Email, x => x.Name)
                .VisibleOn(x => x.NotEquals(x => x.Email, null));
        }
    }

    public record Person(string Name);
}