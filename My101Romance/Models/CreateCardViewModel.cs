using System.ComponentModel.DataAnnotations;

public class CreateCardViewModel
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string ImagePath { get; set; }

    public bool IsForAll { get; set; } = true;

    public int Rating { get; set; } = 0;
}