using System;
using System.Collections.Generic;

namespace BandagingWebApplication.Models;

public partial class SubCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual Category? Category { get; set; }
}
