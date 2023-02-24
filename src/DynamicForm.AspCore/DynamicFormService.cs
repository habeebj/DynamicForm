namespace DynamicForm.AspCore;
public class DynamicFormService : IDynamicFormService
{
    private readonly IEnumerable<FormCollection> _formContexts;

    public DynamicFormService(IEnumerable<FormCollection> formContexts)
    {
        _formContexts = formContexts;
    }

    public IEnumerable<object> GetForms()
    {
        return _formContexts.Select(x => new { Id = x.CollectionName, FormContext = x.GetType().Name });
    }

    public Dictionary<string, object>? GetForm(string id) =>
        _formContexts.FirstOrDefault(x => string.Compare(x.CollectionName, id, ignoreCase: true) == 0)?.Build();
}
