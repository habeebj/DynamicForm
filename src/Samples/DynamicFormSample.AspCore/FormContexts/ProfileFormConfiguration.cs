using DynamicForm;
using DynamicForm.Interfaces;
using DynamicFormSample.AspCore.Models;

namespace DynamicFormSample.AspCore.FormContexts
{
    public interface IMerchantOnboarding { }
    public class ProfileFormConfiguration : FormConfiguration<Profile>, IMerchantOnboarding
    {
        public override void OnConfigure(IFormBuilder<Profile> builder)
        {
            builder
                .TextField(x => x.LastName)
                .Label("Last Name")
                .Placeholder("Enter Last Name")
                .WithValidation(x => x.Required());

            builder
                .TextField(x => x.FirstName)
                .Label("First Name")
                .Placeholder("Enter First Name")
                .WithValidation(x => x.Required());

            builder
                .NumberField(x => x.Age)
                .Label("Age")
                .WithValidation(x => x.MinLength(18).MaxLength(99).Required());

            builder
                .TextAreaField(x => x.Biography)
                .Label("Biography");
        }

        public override void Setup()
        {
            Index(1);
            Api(HttpMethod.Put, "/accounts/profile");
            PreRequest<User, string>(0, x => x.Id, x => x.UserId);
        }
    }
}