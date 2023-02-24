using DynamicForm.Interfaces;
using FluentAssertions;

namespace DynamicForm.Tests
{
    public class InputBuilderTests
    {
        [Fact]
        public void InputBuilderConstructor_ValidInput_ShouldBeSuccessful()
        {
            var idKey = "id";
            var typeKey = "type";
            var id = "name";
            var type = InputType.Text.ToString();

            var builder = new InputBuilder<User, object>(id, type);
            var actual = builder.Build();

            actual.Should().ContainKeys(idKey, typeKey);
            actual[idKey].Should().Be(id);
            actual[typeKey].Should().Be(type.ToLower());
        }

        [Fact]
        public void InputBuilder_WithValidAttributes_ShouldBeSuccessful()
        {
            var labelKey = "label";
            var placeholderKey = "placeholder";
            var label = "First Name";
            var placeholder = "Enter First Name";

            IInputBuilder<User, object> builder = new InputBuilder<User, object>(Keys.NAME, InputType.Text.ToString());
            builder.Label(label).Placeholder(placeholder);
            var actual = ((IBuilder)builder).Build();

            actual.Should().ContainKeys(labelKey, placeholderKey);
            actual[labelKey].Should().Be(label);
            actual[placeholderKey].Should().Be(placeholder);
        }

        [Fact]
        public void InputBuilder_DependsOn_ShouldBeSuccessful()
        {
            IInputBuilder<User, object> builder = new InputBuilder<User, object>(Keys.NAME, InputType.Text.ToString());
            builder.DependsOn(x => x.Email);
            var actual = ((IBuilder)builder).Build();

            actual.Should().ContainKeys(Keys.DEPENDS_ON);
            ((string[])actual[Keys.DEPENDS_ON]).Should().HaveCount(1);
            ((string[])actual[Keys.DEPENDS_ON]).Should().Contain(nameof(User.Email));
        }
        
        [Fact]
        public void OptionBuilder_WithOptions_ShouldBeSuccessful()
        {
            var labelKey = "label";
            var optionsKey = "options";
            var label = "First Name";
            var options = new[] { "A", "B", "C" };

            IOptionBuilder<User, object> builder = new InputBuilder<User, object>(Keys.NAME, InputType.Text.ToString());
            builder.AddOptions(options).Label(label);
            var actual = ((IBuilder)builder).Build();

            actual.Should().ContainKeys(optionsKey, labelKey);
            actual[labelKey].Should().Be(label);
            actual[optionsKey].Should().Be(options);
        }

        [Fact]
        public void OptionBuilder_WithValidUrlString_ShouldBeSuccessful()
        {
            var labelKey = "label";
            var pullUrlKey = "pullUrl";
            var selectDataAccessorKey = "selectDataAccessor";
            var label = "First Name";
            var url = "http://api.example.com/users";

            IOptionBuilder<User, object> builder = new InputBuilder<User, object>(Keys.NAME, InputType.Text.ToString());
            builder.WithUrl(url, "data.result").Label(label);
            var actual = ((IBuilder)builder).Build();

            actual.Should().ContainKeys(pullUrlKey, selectDataAccessorKey, labelKey);
            actual[labelKey].Should().Be(label);
            actual[pullUrlKey].Should().Be(url);
            (actual[selectDataAccessorKey] as string[]).Should().Contain("data", "result");
        }

        [Fact]
        public void OptionBuilder_WithValidUrl_ShouldBeSuccessful()
        {
            var labelKey = "label";
            var pullUrlKey = "pullUrl";
            var selectDataAccessorKey = "selectDataAccessor";
            var label = "First Name";
            Uri.TryCreate("http://api.example.com/users", UriKind.RelativeOrAbsolute, out Uri? url);

            IOptionBuilder<User, object> builder = new InputBuilder<User, object>(Keys.NAME, InputType.Text.ToString());
            builder.WithUrl<Response<User[]>>(url!, x => x.Data).Label(label);
            var actual = ((IBuilder)builder).Build();

            actual.Should().ContainKeys(pullUrlKey, selectDataAccessorKey, labelKey);
            actual[labelKey].Should().Be(label);
            actual[pullUrlKey].Should().Be(url!.ToString());
            (actual[selectDataAccessorKey] as List<string>).Should().ContainSingle("Data");
        }

        [Fact]
        public void OptionBuilder_WithInValidUrlString_ShouldBeFail()
        {
            var url = "http:api.example .com/users";

            IOptionBuilder<User, object> builder = new InputBuilder<User, object>(Keys.NAME, InputType.Text.ToString());

            Action act = () => builder.WithUrl(url, "data.result");
            act.Should().Throw<ArgumentException>().WithMessage("Invalid URI string");
        }
    }
}