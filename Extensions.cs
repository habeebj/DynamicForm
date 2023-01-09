namespace dynamic_form
{
    public static class Extensions
    {
        public static IInputValidator<int> AllowNegative(this IInputValidator<int> validator)
        {
            ((IContentSetter)validator).Set("negative", true);
            return validator;
        }
    }
}