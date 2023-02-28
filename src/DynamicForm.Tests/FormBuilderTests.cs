using DynamicForm.Interfaces;
using FluentAssertions;

namespace DynamicForm.Tests
{
    public class FormBuilderTests
    {
        [Fact]
        public void CreateForm_WithForm_ShouldBeSuccessful()
        {
            IFormBuilder<User> userFormBuilder = new FormBuilder<User>();

            userFormBuilder.FormInput(x => x.Contacts)
                .WithForm(x =>
                {
                    x.TextAreaField(x => x.City)
                    .Label("");

                    x.TextField(x => x.Address)
                        .Label("Address");
                })
                .Label("Contacts");

            var act = ((IBuilder)userFormBuilder).Build();
            // var formInputs = (act[Keys.FORM] as IEnumerable<Dictionary<string, object>>)!.ToList();
            // var emailInput = formInputs[0];

            // act.Should().ContainKey(Keys.FORM);
            // emailInput.Should().ContainKeys(Keys.ID, Keys.TYPE);
            // (emailInput[Keys.ID] as string).Should().Be(nameof(User.Name));
            // (emailInput[Keys.TYPE] as string).Should().Be(InputType.Text.ToString().ToLower());
        }

        [Fact]
        public void CreatePrimitiveField_ShouldBeSuccessful()
        {
            IFormBuilder<User> userFormBuilder = new FormBuilder<User>();
            userFormBuilder.Property(x => x.Name, InputType.Text);

            var act = ((IBuilder)userFormBuilder).Build();
            var formInputs = (act[Keys.FORM] as IEnumerable<Dictionary<string, object>>)!.ToList();
            var emailInput = formInputs[0];

            act.Should().ContainKey(Keys.FORM);
            emailInput.Should().ContainKeys(Keys.ID, Keys.TYPE);
            (emailInput[Keys.ID] as string).Should().Be(nameof(User.Name));
            (emailInput[Keys.TYPE] as string).Should().Be(InputType.Text.ToString().ToLower());
        }

        [Fact]
        public void CreateFields_ShouldBeSuccessful()
        {
            IFormBuilder<User> userFormBuilder = new FormBuilder<User>();
            userFormBuilder.EmailField(x => x.Email);
            userFormBuilder.TextField(x => x.Name);
            userFormBuilder.TextAreaField(x => x.Biography);
            userFormBuilder.NumberField(x => x.Age);
            userFormBuilder.PasswordField(x => x.Password);
            userFormBuilder.ConfirmField(x => x.Password, InputType.Password);

            var act = ((IBuilder)userFormBuilder).Build();
            var formInputs = (act[Keys.FORM] as IEnumerable<Dictionary<string, object>>)!.ToList();
            var emailInput = formInputs[0];
            var textInput = formInputs[1];
            var textAreaInput = formInputs[2];
            var numberInput = formInputs[3];
            var passwordInput = formInputs[4];
            var confirmFieldInput = formInputs[5];

            act.Should().ContainKey(Keys.FORM);
            (emailInput[Keys.ID] as string).Should().Be(nameof(User.Email));
            (emailInput[Keys.TYPE] as string).Should().Be(InputType.Email.ToString().ToLower());

            textInput.Should().ContainKeys(Keys.ID, Keys.TYPE);
            (textInput[Keys.ID] as string).Should().Be(nameof(User.Name));
            (textInput[Keys.TYPE] as string).Should().Be(InputType.Text.ToString().ToLower());

            textAreaInput.Should().ContainKeys(Keys.ID, Keys.TYPE);
            (textAreaInput[Keys.ID] as string).Should().Be(nameof(User.Biography));
            (textAreaInput[Keys.TYPE] as string).Should().Be(InputType.TextArea.ToString().ToLower());

            numberInput.Should().ContainKeys(Keys.ID, Keys.TYPE);
            (numberInput[Keys.ID] as string).Should().Be(nameof(User.Age));
            (numberInput[Keys.TYPE] as string).Should().Be(InputType.Number.ToString().ToLower());

            passwordInput.Should().ContainKeys(Keys.ID, Keys.TYPE);
            (passwordInput[Keys.ID] as string).Should().Be(nameof(User.Password));
            (passwordInput[Keys.TYPE] as string).Should().Be(InputType.Password.ToString().ToLower());

            confirmFieldInput.Should().ContainKeys(Keys.ID, Keys.TYPE);
            (confirmFieldInput[Keys.ID] as string).Should().Be($"Confirm{nameof(User.Password)}");
            (confirmFieldInput[Keys.TYPE] as string).Should().Be(InputType.Password.ToString().ToLower());
        }

        [Fact]
        public void CreateField_WithValidation_ShouldBeSuccessful()
        {
            IFormBuilder<User> userFormBuilder = new FormBuilder<User>();
            userFormBuilder
                .EmailField(x => x.Email)
                .WithValidation(x => x.Required().MinLength(18).MaxLength(99));

            var act = ((IBuilder)userFormBuilder).Build();
            var formInputs = (act[Keys.FORM] as IEnumerable<Dictionary<string, object>>)!.ToList();
            var emailInput = formInputs[0];

            act.Should().ContainKey(Keys.FORM);
            emailInput.Should().ContainKeys(Keys.ID, Keys.TYPE);
            (emailInput[Keys.ID] as string).Should().Be(nameof(User.Email));
            (emailInput[Keys.TYPE] as string).Should().Be(InputType.Email.ToString().ToLower());
        }
    }
}