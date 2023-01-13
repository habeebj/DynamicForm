namespace dynamic_form
{
    public class LoginFormConfiguration : IFormConfiguration<User>
    {
        public void Configure(IFormBuilder<User> builder)
        {
            builder.EmailField(x => x.Email)
                .Label("Email Address")
                .WithValidation(x => x.Required());

            builder.PasswordField(x => x.Password)
                .Label("Password")
                .WithValidation(x => x.MinLength(10));

            builder.ConfirmField(x => x.Password, InputType.Password)
                .Label("Password");
        }
    }
}