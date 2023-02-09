using DynamicForm.Interfaces;
using FluentAssertions;

namespace DynamicForm.Tests
{
    public class InputValidatorTests
    {
        [Fact]
        public void ValidationBuilderTest_ShouldBeSuccessful()
        {
            var validationBuilder = new InputValidator<string>();
            validationBuilder
                .Required()
                .MaxLength(99)
                .MinLength(18)
                .OneOf(new string[] { "A", "B", "C" });

            var actual = ((IBuilder)validationBuilder).Build();
            actual.Should().ContainKeys(Keys.REQUIRED, Keys.MAX, Keys.MIN);
            
            ((bool)actual[Keys.REQUIRED]).Should().BeTrue();
            ((int)actual[Keys.MAX]).Should().Be(99);
            ((int)actual[Keys.MIN]).Should().Be(18);
        }
    }
}