﻿using dynamic_form;

// var formModelBuilder = new FormModelBuilder();
// formModelBuilder.ApplyConfiguration(new LoginFormConfiguration());
// var output = ((IBuilder)formModelBuilder).Build(new Serializer());
// Console.WriteLine(output);

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

Console.WriteLine($"Text");


// // TODO: allow multiple
// userFormBuilder
//     .CheckBox(x => x.Interests)
//     .Options(Lookups.Interests)
//     .Label("Test");

// userFormBuilder
//     .Select(x => x.Interests)
//     .Data<Response<User[]>>(new Uri("http://example.com"), x => x.Data)
//     .Label("Test");

// userFormBuilder
//     .PasswordField(x => x.Password)
//     .WithValidation(x => x.Required().MinLength(10))
//     .Label("Password");

// userFormBuilder
//     .ConfirmField(x => x.Password, InputType.Password) // infer input type from reference
//     .Placeholder("Confirm Password")
//     .WithValidation(x => x.Required())
//     .Label("Confirm Password");

// var output = ((IBuilder)userFormBuilder).Build(new Serializer());
// Console.WriteLine(output);