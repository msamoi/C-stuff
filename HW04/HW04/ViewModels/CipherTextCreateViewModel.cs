using WebApp.Models;

namespace WebApp.ViewModels;

public class CipherTextCreateViewModel
{
    public string Text { get; set; }
    public Guid KeyId { get; set; }
    public int EncTypeId { get; set; }
}