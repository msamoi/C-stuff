using System.ComponentModel.DataAnnotations;
namespace WebApp.ViewModels;

public class PlainTextCreateViewModel
{
    [MaxLength(256)]
    public string Text { get; set; }
    
    public Guid KeyId { get; set; }
}