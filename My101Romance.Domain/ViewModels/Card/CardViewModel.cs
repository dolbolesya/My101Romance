namespace My101Romance.Domain.ViewModels.Card;

public class CardViewModel
{
    public int Id { get; set; }
    
    public string Title { get; set; } 
    
    public string Description { get; set; }

    public int Rating { get; set; } 

    public bool IsForAll { get; set; }
}