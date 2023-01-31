using System.Reflection;
using DynamicForm;
using DynamicForm.Interfaces;

var formCollectionBuilder = new FormCollectionBuilder();
formCollectionBuilder.ApplyConfiguration(Assembly.GetExecutingAssembly());
var output = ((IBuilder)formCollectionBuilder).Build();
Console.WriteLine(Serializer.Serialize(output));