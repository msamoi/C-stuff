using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels;

public class KeyCreateViewModel
{
    [MaxLength(256)]
    public string Text { get; set; }

    public int EncTypeId { get; set; }
}