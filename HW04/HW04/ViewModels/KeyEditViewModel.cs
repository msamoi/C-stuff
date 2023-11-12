using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class KeyEditViewModel
{
    public Guid Id { get; set; }
    [MaxLength(256)]
    public string Text { get; set; }

    public int EncTypeId { get; set; }
}