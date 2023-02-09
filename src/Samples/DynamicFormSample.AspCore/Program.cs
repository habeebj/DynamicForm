using System.Reflection;
using DynamicForm.AspCore;
using DynamicFormSample.AspCore.FormContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDynamicForm(options =>
{
    // options.AddContextFromAssembly(Assembly.GetExecutingAssembly());
    options.AddContext<LoginContext>();
    options.AddContext<MerchantOnBoardingContext>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDynamicForm();

app.Run();
