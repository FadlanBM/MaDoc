using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbDokuman> TbDokumen { get; set; }

    public virtual DbSet<TbHistory> TbHistories { get; set; }

    public virtual DbSet<TbIdentita> TbIdentitas { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-8UJEI4N\\MSSQLSERVER01;Initial Catalog=DokumenManagement;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbDokuman>(entity =>
        {
            entity.HasKey(e => e.IdDokumen).HasName("PK_tb_dockumen");

            entity.ToTable("tb_dokumen");

            entity.Property(e => e.IdDokumen).HasColumnName("id_dokumen");
            entity.Property(e => e.AgendaDokumen)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("agendaDokumen");
            entity.Property(e => e.IdPemilik).HasColumnName("id_pemilik");
            entity.Property(e => e.IdPenerima).HasColumnName("id_penerima");
            entity.Property(e => e.ImagePath)
                .HasColumnType("text")
                .HasColumnName("imagePath");
            entity.Property(e => e.ImageQrCode)
                .HasColumnType("text")
                .HasColumnName("imageQrCode");
            entity.Property(e => e.NameDokumen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nameDokumen");
            entity.Property(e => e.PenerimaPertama)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("penerima_pertama");
            entity.Property(e => e.PengirimDokumen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pengirimDokumen");
            entity.Property(e => e.PerihalDokumen)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("perihalDokumen");
            entity.Property(e => e.TglAgendaAkhir)
                .HasColumnType("datetime")
                .HasColumnName("tgl_agendaAkhir");
            entity.Property(e => e.TglAgendaAwal)
                .HasColumnType("datetime")
                .HasColumnName("tgl_agendaAwal");
            entity.Property(e => e.TglCreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("tgl_createdAt");
            entity.Property(e => e.TglDiterima)
                .HasColumnType("datetime")
                .HasColumnName("tgl_diterima");
            entity.Property(e => e.TglDokumen)
                .HasColumnType("datetime")
                .HasColumnName("tgl_dokumen");
            entity.Property(e => e.TokenDokumen)
                .HasColumnType("text")
                .HasColumnName("token_dokumen");
            entity.Property(e => e.UraianDokumen)
                .HasColumnType("text")
                .HasColumnName("uraianDokumen");

            entity.HasOne(d => d.IdPemilikNavigation).WithMany(p => p.TbDokumanIdPemilikNavigations)
                .HasForeignKey(d => d.IdPemilik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_dokumen_tb_user");

            entity.HasOne(d => d.IdPenerimaNavigation).WithMany(p => p.TbDokumanIdPenerimaNavigations)
                .HasForeignKey(d => d.IdPenerima)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_dokumen_tb_penerima");
        });

        modelBuilder.Entity<TbHistory>(entity =>
        {
            entity.HasKey(e => e.IdHistory).HasName("PK_tb_penerima");

            entity.ToTable("tb_history");

            entity.Property(e => e.IdHistory).HasColumnName("id_history");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.IdDokumen).HasColumnName("id_dokumen");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NamaUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nama_user");

            entity.HasOne(d => d.IdDokumenNavigation).WithMany(p => p.TbHistories)
                .HasForeignKey(d => d.IdDokumen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tb_history_tb_dokumen");
        });

        modelBuilder.Entity<TbIdentita>(entity =>
        {
            entity.HasKey(e => e.IdIdentitas);

            entity.ToTable("tb_identitas");

            entity.Property(e => e.IdIdentitas).HasColumnName("id_identitas");
            entity.Property(e => e.NameIdentitas)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nameIdentitas");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("tb_user");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Alamat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("alamat");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.IdIdentitas).HasColumnName("id_identitas");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NoIdentitas)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("no_identitas");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Verify).HasColumnName("verify");

            entity.HasOne(d => d.IdIdentitasNavigation).WithMany(p => p.TbUsers)
                .HasForeignKey(d => d.IdIdentitas)
                .HasConstraintName("FK_tb_user_tb_identitas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
