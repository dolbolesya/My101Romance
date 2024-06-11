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

    [Display(Name = "Зображення")]
    public string? ImagePath { get; set; } =
        "https://img.freepik.com/free-photo/client-dark-vip-cinema-studio_23-2149500542.jpg?t=st=1716237241~exp=1716240841~hmac=40061bc43f7a8d62bde5c69d531baaa6bf5895ec08424e4ddf554a89b66f860d&w=996";
        //"https://images.prom.ua/1065612508_w640_h640_vafelnaya-kartinka-lyubov.jpg";
    
    [Display(Name = "Рейтинг")]
    public int Rating { get; set; } 
    
    [Display(Name = "Відображення для усіх користувачів")]
    public bool IsForAll { get; set; }

}

