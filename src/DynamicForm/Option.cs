namespace DynamicForm;
public class Option
{
    public Option(string id, string name) => (Id, Name) = (id, name);
    public Option(string name) => (Id, Name) = (name, name);

    public string Id { get; set; }
    public string Name { get; set; }
}