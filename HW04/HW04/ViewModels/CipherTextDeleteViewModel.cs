using WebApp.Models;

namespace WebApp.ViewModels;

public class CipherTextDeleteViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string Key { get; set; }
    public EncType EncType { get; set; }
}