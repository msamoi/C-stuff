using WebApp.Models;

namespace WebApp.ViewModels;

public class CipherTextEditViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    
    public Guid KeyId { get; set; }
}