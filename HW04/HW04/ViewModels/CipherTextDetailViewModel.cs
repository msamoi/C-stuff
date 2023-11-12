using WebApp.Models;

namespace WebApp.ViewModels;

public class CipherTextDetailViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    
    public string? Key { get; set; }

    public string EncType { get; set; }
}