namespace MedApp.Infrastructure.Database.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("addresses")]
public class AddressEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string? Street { get; set; }
    
    [Required]
    public string? City { get; set; }
    
    [Required]
    public string? State { get; set; }
    
    [Required]
    public string? ZipCode { get; set; }
    
    [Required]
    public bool IsMailingAddress { get; set; }

    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime CreatedAt {get; set;}

    [Column(TypeName = "timestamp")]
    public DateTime? UpdatedAt {get; set;}

    public int PatientId {get; set;}

    public PatientEntity? Patient {get; set;}
}