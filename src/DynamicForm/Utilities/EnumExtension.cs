namespace DynamicForm.Utilities
{
    public static class EnumExtension
    {
        public static Option[] ToOptions<TEnum>() where TEnum : Enum
        {
            return Enum.GetNames(typeof(TEnum)).Select(x => new Option(x, x)).ToArray();
        }
    }
}