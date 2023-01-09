using dynamic_form;

IFormBuilder<User> userFormBuilder = new FormBuilder<User>();

userFormBuilder
    .Property(x => x.Age, InputType.Number)
    .Placeholder("Enter your age")
    .WithValidation(x => x.Required().AllowNegative())
    .Label("Age")
    .WithValidation(x => x.MinLength(18).MaxLength(99));

userFormBuilder
    .EmailField(x => x.Email)
    .Placeholder("Enter email address")
    .WithValidation(x => x.Required())
    .Label("Email address");

userFormBuilder
    .CheckBox(x => x.Email);
// .Data<Response<User[]>>(new Uri(""), x => x.Data)
//     .Placeholder("Enter email address")
//     .WithValidation(x => x.Required())
//     .Label("Email address");


var output = ((IBuilder)userFormBuilder).Build(new Serializer());
Console.WriteLine(output);

// userBuilder
//     .EmailField(x => x.Email)
//     .Label("Email Address")
//     .Placeholder("Enter your email address")
//     .WithValidation(x => x.Required().MaxLength(255).MinLength(10))
//     .WithValidation(x => x.OnOf(new[] { "1", "2", "3" }));


// userBuilder
//     .Property(x => x.Age, InputType.Number)
//     .Label(nameof(User.Age))
//     .WithValidation(x => x.AllowNegative().Required())
//     .WithOptions(x => x.Data<Response<User[]>>(new Uri("https://api.com/users"), u => u.Data));




