using System.Reflection;
using DynamicForm;

namespace DynamicFormSample.AspCore.FormContexts
{
    public class MerchantOnBoardingContext : FormContext
    {
        protected override void OnFormCreating(FormCollectionBuilder formBuilder)
        {
            formBuilder.ApplyConfiguration(
                Assembly.GetExecutingAssembly(),
                x => x.IsAssignableTo(typeof(IMerchantOnboarding)));
        }

        protected override void Setup()
        {
            BaseUrl("/");
        }
    }
}