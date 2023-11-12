using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels;

public class KeyDeleteViewModel
{
    public Guid Id { get; set; }

    [MaxLength(256)]
    public string Text { get; set; }

    public EncType EncType { get; set; }
}