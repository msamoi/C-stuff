using Microsoft.AspNetCore.Identity;

namespace WebApp.Models;

public class AppUser : IdentityUser
{
    public IdentityRole? Role { get; set; }
}