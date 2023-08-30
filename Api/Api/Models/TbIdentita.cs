using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class TbIdentita
{
    public int IdIdentitas { get; set; }

    public string NameIdentitas { get; set; } = null!;

    public virtual ICollection<TbUser> TbUsers { get; set; } = new List<TbUser>();
}
