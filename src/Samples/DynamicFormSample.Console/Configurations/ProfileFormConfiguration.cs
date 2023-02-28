using DynamicForm;
using DynamicForm.Interfaces;

namespace Sample.Configurations
{
    public class ProfileFormConfiguration : FormConfiguration<User>
    {
        public override void Setup()
        {
            Index(1);
            Api(HttpMethod.Post, "/profile");
        }

        public override void OnConfigure(IFormBuilder<User> builder)
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

            builder.FormInput(x => x.Contacts)
                .WithForm(x =>
                {
                    x.TextAreaField(x => x.City)
                    .Label("City")
                    .WithValidation(x => x.MaxLength(100));

                    x.TextField(x => x.Address)
                        .Label("Address");

                    x.Select(x => x.State)
                        .AddOptions(new Option[] { new Option("Lagos", "Lagos"), new Option("Abuja", "Abuja") });
                }, x => x.Address, x => x.City)
                .Label("Contacts");
        }
    }
}