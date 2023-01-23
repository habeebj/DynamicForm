## Extending dynamic form validation

```csharp
public static IInputValidator<int> AllowNegative(this IInputValidator<int> validator)
{
    ((IContentSetter)validator).Set("negative", true);
    return validator;
}
```

## TODO: 
- Select Option with Name Value pair
- Additional properties for form and form collections