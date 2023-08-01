namespace MedApp.Infrastructure.Persistence.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public record UserEntity
{
    [Key]
    public long Id {get; set;}
    
    [Required]
    public string? FirstName {get; set;}

    [Required]
    public string? LastName {get; set;}

    [Required]
    public string? Username {get; set;}

    [Required]
    public string? Email {get; set;}

    [Required]
    public string? PasswordSalt {get; set;}

    [Required]
    public string? PasswordHash {get; set;}
    
    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime CreatedAt {get; set;}

    [Column(TypeName = "timestamp")]
    public DateTime? UpdatedAt {get; set;}
}