using System.Reflection;

namespace DynamicForm.AspCore.Options;
public class DynamicFormOptions
{
    public List<FormCollection> formCollections { internal get; set; } = new();

    public void AddCollection<T>() where T : FormCollection
    {
        formCollections.Add(Activator.CreateInstance<T>());
    }

    public void AddCollection<T>(T type) where T : FormCollection
    {
        AddCollection(type);
    }

    private void AddCollection(Type type)
    {
        if (Activator.CreateInstance(type) is FormCollection formContext)
        {
            formCollections.Add(formContext);
        }
    }

    public void AddCollectionFromAssembly(Assembly assembly)
    {
        var types = assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(FormCollection)));
        foreach (var type in types)
        {
            AddCollection(type);
        }
    }

<<<<<<< HEAD
    public void AddCollectionFromAssembly(params Assembly[] assemblies)
=======
    public void AddCollectionFromAssembly(IEnumerable<Assembly> assemblies)
>>>>>>> 4cc06a15b589b8039b64847d08dbdbbcecda2e9e
    {
        foreach (var assembly in assemblies)
        {
            AddCollectionFromAssembly(assembly);
        }
    }
}
