using System.Reflection;
using DynamicForm.Interfaces;
using DynamicForm.Tests.Configurations;
using FluentAssertions;

namespace DynamicForm.Tests
{
    public class FormCollectionBuilderTests
    {
        [Fact]
        public void FormCollectionBuilder_ApplyConfiguration_ShouldBeSuccessful()
        {
            var formCollectionBuilder = new FormCollectionBuilder();
            formCollectionBuilder.ApplyConfiguration(new LoginFormConfiguration());
            formCollectionBuilder.ApplyConfiguration(new SignUpFormConfiguration());

            var actual = ((IBuilder)formCollectionBuilder).Build();
            var forms = (actual[Keys.DATA] as IEnumerable<Dictionary<string, object>>)!.ToList();
            forms.Should().HaveCount(2);
        }

        [Fact]
        public void FormCollectionBuilder_ApplyConfigurationFromAssembly_ShouldBeSuccessful()
        {
            var formCollectionBuilder = new FormCollectionBuilder();
            formCollectionBuilder.ApplyConfiguration(Assembly.GetExecutingAssembly(), x => x.IsAssignableTo(typeof(IScannable)));

            var actual = ((IBuilder)formCollectionBuilder).Build();
            var forms = (actual[Keys.DATA] as IEnumerable<Dictionary<string, object>>)!.ToList();
            forms.Should().HaveCount(1);
        }
    }
}