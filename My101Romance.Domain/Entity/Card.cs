using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace My101Romance.Domain.Entity;

public class Card 
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    
    [Display(Name = "Заголовок")]
    public string? Title { get; set; } 
    
    
    [Display(Name = "Опис")]
    public string? Description { get; set; }

    //[Display(Name = "Зображення")]
    //public string? ImagePath { get; set; }
    
    [Display(Name = "Рейтинг")]
    public int Rating { get; set; } 

    
    [Display(Name = "Відображення для усіх користувачів")]
    public bool IsForAll { get; set; }


}
