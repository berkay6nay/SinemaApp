using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SinemaApp.Models;

public partial class Sinema2Context : DbContext
{
    public Sinema2Context()
    {
    }

    public Sinema2Context(DbContextOptions<Sinema2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Gosterim> Gosterims { get; set; }

    public virtual DbSet<GösterimKoltuk> GösterimKoltuks { get; set; }

    public virtual DbSet<Kullanici> Kullanicis { get; set; }

    public virtual DbSet<Rapor1> Rapor1s { get; set; }

    public virtual DbSet<Rezervasyon> Rezervasyons { get; set; }

    public virtual DbSet<Salon> Salons { get; set; }

    public virtual DbSet<Sinema> Sinemas { get; set; }

    public virtual DbSet<SinemaSalonuKoltuk> SinemaSalonuKoltuks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-G82KLUL;Initial Catalog=Sinema2;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin__3213E83F05B93B38");

            entity.ToTable("Admin");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Rol)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("rol");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Admin)
                .HasForeignKey<Admin>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Admin__id__3B75D760");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Film__3213E83F4A7D5366");

            entity.ToTable("Film");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aciklama).HasColumnName("aciklama");
            entity.Property(e => e.CikisTarihi)
                .HasColumnType("datetime")
                .HasColumnName("cikisTarihi");
            entity.Property(e => e.Dil)
                .HasMaxLength(50)
                .HasColumnName("dil");
            entity.Property(e => e.FotoUrl)
                .HasMaxLength(255)
                .HasColumnName("foto_url");
            entity.Property(e => e.Isim)
                .HasMaxLength(255)
                .HasColumnName("isim");
            entity.Property(e => e.Sure).HasColumnName("sure");
            entity.Property(e => e.Tur)
                .HasMaxLength(100)
                .HasColumnName("tur");
        });

        modelBuilder.Entity<Gosterim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gosterim__3213E83FA5DEF6F5");

            entity.ToTable("Gosterim");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baslangic)
                .HasColumnType("datetime")
                .HasColumnName("baslangic");
            entity.Property(e => e.Bitis)
                .HasColumnType("datetime")
                .HasColumnName("bitis");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.SalonId).HasColumnName("salon_id");

            entity.HasOne(d => d.Film).WithMany(p => p.Gosterims)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK__Gosterim__film_i__45F365D3");

            entity.HasOne(d => d.Salon).WithMany(p => p.Gosterims)
                .HasForeignKey(d => d.SalonId)
                .HasConstraintName("FK__Gosterim__salon___46E78A0C");
        });

        modelBuilder.Entity<GösterimKoltuk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gösterim__3213E83F2A57E1A4");

            entity.ToTable("GösterimKoltuk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Durum).HasColumnName("durum");
            entity.Property(e => e.Fiyat).HasColumnName("fiyat");
            entity.Property(e => e.GosterimId).HasColumnName("gosterim_id");
            entity.Property(e => e.RezervasyonId).HasColumnName("rezervasyon_id");
            entity.Property(e => e.SinemaSalonuKoltukId).HasColumnName("sinemaSalonuKoltuk_id");

            entity.HasOne(d => d.Gosterim).WithMany(p => p.GösterimKoltuks)
                .HasForeignKey(d => d.GosterimId)
                .HasConstraintName("FK__GösterimK__goste__5FB337D6");

            entity.HasOne(d => d.Rezervasyon).WithMany(p => p.GösterimKoltuks)
                .HasForeignKey(d => d.RezervasyonId)
                .HasConstraintName("FK_GosterimKoltuk_Rezervasyon");

            entity.HasOne(d => d.SinemaSalonuKoltuk).WithMany(p => p.GösterimKoltuks)
                .HasForeignKey(d => d.SinemaSalonuKoltukId)
                .HasConstraintName("FK__GösterimK__sinem__5EBF139D");
        });

        modelBuilder.Entity<Kullanici>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kullanic__3213E83FC5B77AF1");

            entity.ToTable("Kullanici");

            entity.HasIndex(e => e.Isim, "UQ__Kullanic__99FA9B90FB68F94F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isim)
                .HasMaxLength(255)
                .HasColumnName("isim");
            entity.Property(e => e.Rol)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("rol");
            entity.Property(e => e.Sifre)
                .HasMaxLength(255)
                .HasColumnName("sifre");
            entity.Property(e => e.TelNo)
                .HasMaxLength(50)
                .HasColumnName("telNo");
        });

        modelBuilder.Entity<Rapor1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Rapor1");

            entity.Property(e => e.Baslangic)
                .HasColumnType("datetime")
                .HasColumnName("baslangic");
            entity.Property(e => e.Bitis)
                .HasColumnType("datetime")
                .HasColumnName("bitis");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isim)
                .HasMaxLength(255)
                .HasColumnName("isim");
            entity.Property(e => e.SalonId).HasColumnName("salon_id");
            entity.Property(e => e.Sira)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sira");
        });

        modelBuilder.Entity<Rezervasyon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rezervas__3213E83FB7413C3E");

            entity.ToTable("Rezervasyon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GosterimId).HasColumnName("gosterim_id");
            entity.Property(e => e.KoltukSayisi).HasColumnName("koltukSayisi");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");
            entity.Property(e => e.OlusturulmaTarihi)
                .HasColumnType("datetime")
                .HasColumnName("olusturulmaTarihi");

            entity.HasOne(d => d.Gosterim).WithMany(p => p.Rezervasyons)
                .HasForeignKey(d => d.GosterimId)
                .HasConstraintName("FK__Rezervasy__goste__4E88ABD4");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Rezervasyons)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("FK__Rezervasy__kulla__4D94879B");
        });

        modelBuilder.Entity<Salon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Salon__3213E83FEF89BB66");

            entity.ToTable("Salon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isim)
                .HasMaxLength(255)
                .HasColumnName("isim");
            entity.Property(e => e.SinemaId).HasColumnName("sinema_id");
            entity.Property(e => e.ToplamKoltuk).HasColumnName("toplamKoltuk");

            entity.HasOne(d => d.Sinema).WithMany(p => p.Salons)
                .HasForeignKey(d => d.SinemaId)
                .HasConstraintName("FK__Salon__sinema_id__4316F928");
        });

        modelBuilder.Entity<Sinema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sinema__3213E83F3AD01236");

            entity.ToTable("Sinema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adres)
                .HasMaxLength(255)
                .HasColumnName("adres");
            entity.Property(e => e.Isim)
                .HasMaxLength(255)
                .HasColumnName("isim");
            entity.Property(e => e.SalonSayisi).HasColumnName("salonSayisi");
        });

        modelBuilder.Entity<SinemaSalonuKoltuk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SinemaSa__3213E83FA675D83D");

            entity.ToTable("SinemaSalonuKoltuk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SalonId).HasColumnName("salon_id");
            entity.Property(e => e.Sira)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sira");
            entity.Property(e => e.SiraNo).HasColumnName("siraNo");

            entity.HasOne(d => d.Salon).WithMany(p => p.SinemaSalonuKoltuks)
                .HasForeignKey(d => d.SalonId)
                .HasConstraintName("FK__SinemaSal__salon__5BE2A6F2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
