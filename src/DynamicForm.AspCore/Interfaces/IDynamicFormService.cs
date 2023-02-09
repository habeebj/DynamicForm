namespace DynamicForm.AspCore;
public interface IDynamicFormService
{
    IEnumerable<object> GetForms();
    Dictionary<string, object>? GetForm(string id);
}