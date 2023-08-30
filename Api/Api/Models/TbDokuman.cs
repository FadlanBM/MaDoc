using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class TbDokuman
{
    public int IdDokumen { get; set; }

    public string NameDokumen { get; set; } = null!;

    public string AgendaDokumen { get; set; } = null!;

    public string PerihalDokumen { get; set; } = null!;

    public string PengirimDokumen { get; set; } = null!;

    public string TokenDokumen { get; set; } = null!;

    public string PenerimaPertama { get; set; } = null!;

    public int IdPemilik { get; set; }

    public int IdPenerima { get; set; }

    public string UraianDokumen { get; set; } = null!;

    public DateTime TglDiterima { get; set; }

    public DateTime TglDokumen { get; set; }

    public DateTime TglAgendaAwal { get; set; }

    public DateTime TglAgendaAkhir { get; set; }

    public DateTime TglCreatedAt { get; set; }

    public string? ImagePath { get; set; }

    public string? ImageQrCode { get; set; }

    public virtual TbUser IdPemilikNavigation { get; set; } = null!;

    public virtual TbUser IdPenerimaNavigation { get; set; } = null!;

    public virtual ICollection<TbHistory> TbHistories { get; set; } = new List<TbHistory>();
}
