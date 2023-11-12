using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class CipherText
{
    public Guid Id { get; set; }

    [MaxLength(256)]
    public string Text { get; set; }
    
    public Guid KeyId { get; set; }
    public Key? Key { get; set; }
    
    public string UserId { get; set; }
    public AppUser? User { get; set; }
}