﻿using System.ComponentModel.DataAnnotations;

namespace Cards.Models;
public class Card
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    [RegularExpression(@"^#[a-zA-Z0-9]{6}$", ErrorMessage = "The format should be 6 alphanumeric characters prefixed with a #.")]
    public string? Color { get; set; }
    public  required string Status { get; set; }
    public required string CreatedBy { get; set; }
    public required DateTime DateCreated { get; set; }
}
