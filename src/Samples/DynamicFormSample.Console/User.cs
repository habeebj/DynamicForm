namespace Sample
{
    public class User
    {
        public int Age { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Biography { get; set; } = null!;
        public List<Contact> Contacts { get; set; } = new();
        public IEnumerable<string> Interests = new List<string> { "test..." };
    }
}