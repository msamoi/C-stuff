using Microsoft.AspNetCore.Identity;

namespace WebApp.Models;

public class AppUser : IdentityUser
{
    public ICollection<CipherText>? Ciphertext { get; set; }

    public IdentityRole? Role { get; set; }
}