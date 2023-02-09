using FluentAssertions;

namespace DynamicForm.Tests
{
    public class FormContextTests
    {
        [Fact]
        public void Test()
        {
            var context = new TestFormContext();
            var actual = context.Build();
            actual[Keys.NAME].Should().BeOfType<string>();
            actual[Keys.BASE_URL].Should().Be("https://example.com");
            actual[Keys.DESCRIPTION].Should().Be("a sample description");
            var forms = (actual[Keys.DATA] as IEnumerable<Dictionary<string, object>>)!.ToList();
            forms.Should().HaveCount(1);
        }
    }

    public class TestFormContext : FormContext
    {
        protected override void OnFormCreating(FormCollectionBuilder formBuilder)
        {
            formBuilder.ApplyConfiguration(new LoginFormConfiguration());
        }

        protected override void Setup()
        {
            Name("test context");
            BaseUrl("https://example.com");
            Description("a sample description");
        }
    }
}