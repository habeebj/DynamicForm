using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using DynamicForm.AspCore.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DynamicForm.AspCore;
public static class Extensions
{
    /// <summary>
    /// REgister dynamic form endpoint with default endpoint GET /forms
    /// </summary>
    /// <param name="app"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder UseDynamicForm(this IEndpointRouteBuilder app, [StringSyntax("Route")] string pattern = "/forms")
    {
        pattern = pattern.Trim().TrimEnd('/');
        app.MapGet(pattern, (IDynamicFormService dynamicFormService) => dynamicFormService.GetForms());
        app.MapGet(pattern + "/{id}", (string id, IDynamicFormService dynamicFormService) =>
        {
            return dynamicFormService.GetForm(id) is Dictionary<string, object> formContext ?
                Results.Ok(formContext) :
                Results.NotFound();
        });

        return app;
    }

    /// <summary>
    /// Add dynamic form
    /// Scans executing assembly for formContext when options is null
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection AddDynamicForm(this IServiceCollection services, Action<DynamicFormOptions>? options = null)
    {
        var opts = new DynamicFormOptions();
        if (options is null)
        {
            opts.AddCollectionFromAssembly(Assembly.GetExecutingAssembly());
        }

        options?.Invoke(opts);

        services.TryAddSingleton<IEnumerable<FormCollection>>(opts.formCollections);
        services.TryAddSingleton<IDynamicFormService, DynamicFormService>();

        return services;
    }
}
