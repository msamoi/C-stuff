using WebApp.Models;

namespace WebApp.ViewModels;

public class KeyViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public EncType EncType { get; set; }
}