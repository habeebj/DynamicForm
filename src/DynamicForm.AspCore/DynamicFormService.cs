using System.Collections;
using System.Collections.Generic;
namespace DynamicForm.AspCore;
public class DynamicFormService : IDynamicFormService
{
    private readonly IEnumerable<FormContext> _formContexts;

    public DynamicFormService(IEnumerable<FormContext> formContexts)
    {
        _formContexts = formContexts;
    }

    public IEnumerable<object> GetForms()
    {
        return _formContexts.Select(x => new { Id = x.ContextName, FormContext = x.GetType().Name });
    }

    public Dictionary<string, object>? GetForm(string id) =>
        _formContexts.FirstOrDefault(x => string.Compare(x.ContextName, id, ignoreCase: true) == 0)?.Build();
}
