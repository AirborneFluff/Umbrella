﻿using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOs;

public sealed class NewStockItemDto
{
    [Required]
    public required string PartCode { get; set; }
    [Required]
    public required string Description { get; set; }
    public string? Location { get; set; }
    public string? Category { get; set; }
}