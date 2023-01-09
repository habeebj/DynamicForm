namespace dynamic_form
{
    public class Response<T>
    {
        public T Data { get; set; } = default!;
    }

    public class User
    {
        public int Age { get; set; }
        public string Email { get; set; } = null!;
    }
}