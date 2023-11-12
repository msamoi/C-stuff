namespace WebApp.ViewModels;

public class CipherTextDeleteViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string? Key { get; set; }
    public string EncType { get; set; }
}