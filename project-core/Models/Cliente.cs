using System.ComponentModel.DataAnnotations;

namespace project_core.Models;

public class Cliente
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;
}
