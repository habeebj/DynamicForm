namespace DynamicForm.AspCore;
public class DynamicFormService : IDynamicFormService
{
    private readonly IEnumerable<FormCollection> _formCollection;

    public DynamicFormService(IEnumerable<FormCollection> formContexts)
    {
        _formCollection = formContexts;
    }

    public IEnumerable<object> GetForms()
    {
        return _formCollection.Select(x => new { Id = x.CollectionName, FormContext = x.GetType().Name });
    }

    public Dictionary<string, object>? GetForm(string id) =>
        _formCollection.FirstOrDefault(x => string.Compare(x.CollectionName, id, ignoreCase: true) == 0)?.Build();
}
