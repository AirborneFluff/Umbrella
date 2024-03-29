﻿using System.Collections.ObjectModel;

namespace API.Entities;

public sealed class Organisation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string OwnerId { get; set; }
    public required string Name { get; set; }
    
    public string NormalizedName
    {
        get => Name.ToUpper();
        set {}
    }

    public ICollection<AppUser> Members { get; set; } = new List<AppUser>();
}