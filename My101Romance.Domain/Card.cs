namespace My101Romance.Domain;

public class Card
{
    public int? Id { get; init; } = null!;

    public string? Title { get; set; } = null!;
    
    public string? Description { get; set; }

    public int Rating { get; set; } = 0;

    public bool IsForAll { get; set; } = true;
}