# Usage

## ASP Core Usage

```csharp
builder.Services
    .AddDynamicForm(options => {
        // scan assembly for formContext
        options.FromAssembly(Assembly.ExecutingAssembly());
        // register individually
        options.UseContext<SignUpFormContext>();
        options.UseContext<OnBoardingFormContext>();
    });

builder.Services.AddDynamicForm();

// use default /forms endpoint
app.UseDynamicForm();

app.UseDynamicForm("/my-forms");
```

## Form Configuration

```csharp
public class LoginFormConfiguration : FormConfiguration<User>
{
    public override void Setup()
    {
        Name("Login Form");
        Api(HttpMethod.Post, "/accounts");
    }

    public override void OnConfigure(IFormBuilder<User> builder)
    {
        builder
            .EmailField(x => x.Email)
            .Label("Email Address")
            .Placeholder("Enter Email address")
            .WithValidation(x => x.Required());

        builder
            .PasswordField(x => x.Password)
            .Label("Password")
            .WithValidation(x => x.MinLength(10));
    }
}
```

## Form context

```csharp
public class OnboardingFormContext : FormContext
{
    protected override void Setup()
    {
        Name("Merchant Onboarding");
        Description("Merchant Onboarding");
        BaseUrl("https://api.server.com");
    }

    protected override void OnFormCreating(FormCollectionBuilder formBuilder)
    {
        formBuilder.ApplyConfiguration(new LoginFormConfiguration());
        formBuilder.ApplyConfiguration(new ProfileFormConfiguration());
    }
}
```

# Validations

## Extending dynamic form validation

```csharp
public static IInputValidator<int> AllowNegative(this IInputValidator<int> validator)
{
    ((IContentSetter)validator).Set("negative", true);
    return validator;
}
```

## TODO:

- Pre request ✅
- ASP Core Library - Test
  - DynamicFormOptions
  - DynamicFormService
  - Extensions ✅
- Select Option with Name Value pair ✅
- Customizable key for all properties
- Display property ✅
- Additional properties for form and form collections ✅
- ASP Core library to expose form endpoint ✅

[Proof Of Concept](https://hackmd.io/ANY0TF8wT-ape0LOASalvQ)
