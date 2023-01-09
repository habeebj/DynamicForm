## Extending dynamic form validation

```csharp
public static IInputValidator<int> AllowNegative(this IInputValidator<int> validator)
{
    ((IContentSetter)validator).Set("negative", true);
    return validator;
}
```
