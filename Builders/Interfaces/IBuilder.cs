namespace dynamic_form
{
    public interface IBuilder
    {
        Dictionary<string, object> Build();
        // string Build(ISerializer serializer);
    }
}