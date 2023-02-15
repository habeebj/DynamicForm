namespace DynamicFormSample.AspCore.Models;

public class Profile
{
    public int Age { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Biography { get; set; } = null!;
    public IEnumerable<string> Interests = new List<string> { "Reading" };

    public string UserId { get; set; } = null!;
}
