
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class AdminStuffViewModel
{
    [MaxLength(64)]
    public string Role { get; set; }
}