namespace WebApp.ViewModels;

public class CipherTextEditViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string? Key { get; set; }
    public int EncTypeId { get; set; }
}