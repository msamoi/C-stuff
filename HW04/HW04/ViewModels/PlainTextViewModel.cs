using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.ViewModels;

public class PlainTextViewModel
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Text { get; set; }
    
    public string EncTypeName { get; set; }
}