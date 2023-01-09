using System.Linq.Expressions;

namespace dynamic_form
{
    public class OptionBuilder<TProperty> : InputBuilder<TProperty>, IOptionBuilder<TProperty>
    {
        public OptionBuilder(string property, string type) : base(property, type)
        {
        }

        public InputBuilder<TProperty> Data(string uri, string property)
        {
            // create Inputbui
            throw new NotImplementedException();
        }

        public InputBuilder<TProperty> Data<TModel>(Uri uri, Expression<Func<TModel, object>> dataPathExpression)
        {
            throw new NotImplementedException();
        }

        public InputBuilder<TProperty> Options(IEnumerable<string> options)
        {
            throw new NotImplementedException();
        }
    }
}