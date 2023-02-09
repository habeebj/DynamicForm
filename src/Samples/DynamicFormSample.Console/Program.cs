using System.Reflection;
using DynamicForm;
using DynamicForm.Interfaces;
using Sample;

// var formCollectionBuilder = new FormCollectionBuilder();
// formCollectionBuilder.ApplyConfiguration(Assembly.GetExecutingAssembly());
// var output = ((IBuilder)formCollectionBuilder).Build();
// Console.WriteLine(Serializer.Serialize(output));

var onboardingContext = new OnboardingFormContext();
var o = onboardingContext.Build();

Console.WriteLine(Serializer.Serialize(o));
