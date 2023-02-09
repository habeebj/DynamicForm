using System.Collections.Generic;

namespace DynamicForm.Tests
{

    public class Lookups
    {
        public static string[] Interests = new[] { "Reading", "Sleeping" };
    }

    public class User
    {
        public int Age { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Biography { get; set; } = null!;
        public IEnumerable<string> Interests = new List<string> { "test..." };
    }
}