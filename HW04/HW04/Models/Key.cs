using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Key
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Text { get; set; }

    public int EncTypeId { get; set; }
    public EncType EncType { get; set; }
    
    public string UserId { get; set; }
    public AppUser? User { get; set; }
}