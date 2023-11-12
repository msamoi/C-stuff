using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class PlainText
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Text { get; set; }
    
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
    
    public Guid CipherTextId { get; set; }
    public CipherText? CipherText { get; set; }
}