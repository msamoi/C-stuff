using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels;

public class PlainTextEditViewModel
{
    public Guid Id { get; set; }

    [MaxLength(256)]
    public string Text { get; set; }

    public Guid KeyId { get; set; }
}