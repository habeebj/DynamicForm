using DynamicForm;
using Sample.Configurations;

namespace Sample
{
    public class OnboardingFormContext : FormContext
    {
        protected override void Setup()
        {
            Name("Merchant Onboarding");
            Description("Merchant Onboarding");
            BaseUrl("https://api.server.com");
        }
        
        protected override void OnFormCreating(FormCollectionBuilder formBuilder)
        {
            formBuilder.ApplyConfiguration(new LoginFormConfiguration());
            formBuilder.ApplyConfiguration(new ProfileFormConfiguration());
        }
    }
}