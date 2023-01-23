using DynamicForm;
using DynamicForm.Interfaces;
using DynamicForm.Tests;
using FluentAssertions;

namespace dynamic_form.DynamicForm.Tests
{
    public class InputValidatorTests
    {
        [Fact]
        public void Test()
        {
            IFormBuilder<User> userFormBuilder = new FormBuilder<User>();
            userFormBuilder
                .Property(x => x.Age, InputType.Number);

            var actualDictionary = ((IBuilder)userFormBuilder).Build();
            actualDictionary.Should().ContainKey("form");
            // inputFields.Should().HaveCount(1);
        }
    }
}