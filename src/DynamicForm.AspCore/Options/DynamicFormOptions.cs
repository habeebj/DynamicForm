using System.Reflection;

namespace DynamicForm.AspCore.Options;
public class DynamicFormOptions
{
    public List<FormContext> formContexts { internal get; set; } = new();

    public void AddContext<T>() where T : FormContext
    {
        formContexts.Add(Activator.CreateInstance<T>());
    }

    public void AddContext<T>(T type) where T : FormContext
    {
        AddContext(type);
    }

    private void AddContext(Type type)
    {
        if (Activator.CreateInstance(type) is FormContext formContext)
        {
            formContexts.Add(formContext);
        }
    }

    public void AddContextFromAssembly(Assembly assembly)
    {
        var types = assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(FormContext)));
        foreach (var type in types)
        {
            AddContext(type);
        }
    }

    public void AddContextFromAssembly(IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            AddContextFromAssembly(assembly);
        }
    }
}
