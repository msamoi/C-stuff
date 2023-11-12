using WebApp.Models;

namespace WebApp.ViewModels;

public class CipherTextCreateViewModel
{
    public string Text { get; set; }
    public string Key { get; set; }
    public int EncTypeId { get; set; }
}