using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class TbHistory
{
    public int IdHistory { get; set; }

    public int IdUser { get; set; }

    public int IdDokumen { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual TbDokuman IdDokumenNavigation { get; set; } = null!;
}
