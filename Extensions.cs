namespace dynamic_form
{
    public static class Extensions
    {
        // TODO: date time validation   
        public static IInputValidator<int> AllowNegative(this IInputValidator<int> validator)
        {
            ((IContentSetter)validator).Set("negative", true);
            return validator;
        }
    }
}