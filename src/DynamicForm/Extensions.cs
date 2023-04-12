using System.Numerics;
using DynamicForm.Interfaces;

namespace DynamicForm
{
    public static class Extensions
    {
        // TODO: date time validation
        public static IInputValidator<TNumber> AllowNegative<TNumber>(this IInputValidator<TNumber> validator, bool allow = true) where TNumber : INumberBase<TNumber>
        {
            ((IContentSetter)validator).Set(Keys.ALLOW_NEGATIVE, allow);
            return validator;
        }
    }
}