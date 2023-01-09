namespace dynamic_form
{
    public interface IBuilder
    {
        string Build(ISerializer serializer);
    }
}