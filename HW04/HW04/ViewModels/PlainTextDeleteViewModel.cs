using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class PlainTextDeleteViewModel
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Text { get; set; }
    
    public Guid KeyId { get; set; }
}