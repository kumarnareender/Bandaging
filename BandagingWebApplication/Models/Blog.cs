using System;
using System.Collections.Generic;

namespace BandagingWebApplication.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string? Headline { get; set; }

    public DateTime? PublishedDate { get; set; }

    public string? Description { get; set; }

    public string? ImageOne { get; set; }

    public string? ImageTwo { get; set; }

    public int? CategoryId { get; set; }

    public virtual SubCategory? Category { get; set; }
}
