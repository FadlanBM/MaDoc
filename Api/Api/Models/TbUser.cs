using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class TbUser
{
    public int IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? NoIdentitas { get; set; }

    public int? IdIdentitas { get; set; }

    public string? Alamat { get; set; }

    public string? PhoneNumber { get; set; }

    public string Password { get; set; } = null!;

    public int Verify { get; set; }

    public int Level { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual TbIdentita? IdIdentitasNavigation { get; set; }

    public virtual ICollection<TbDokuman> TbDokumanIdPemilikNavigations { get; set; } = new List<TbDokuman>();

    public virtual ICollection<TbDokuman> TbDokumanIdPenerimaNavigations { get; set; } = new List<TbDokuman>();
}
