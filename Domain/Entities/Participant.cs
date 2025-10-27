namespace eWorldCup.Domain.Entities;

public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }

    public Participant()
    {
        Name = string.Empty;
    }

    public Participant(int id, string name, string? imageUrl = null)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
    }
}
