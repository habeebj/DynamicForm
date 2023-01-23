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
- Customizable key for all properties
- ASP Core library to expose form endpoint

[Proof Of Concept](https://hackmd.io/ANY0TF8wT-ape0LOASalvQ)