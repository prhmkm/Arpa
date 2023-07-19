using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArpaBackend.Domain.Models
{
    public partial class ArpaWebsite_DBContext : DbContext
    {
        public ArpaWebsite_DBContext()
        {
        }

        public ArpaWebsite_DBContext(DbContextOptions<ArpaWebsite_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Festival> Festivals { get; set; } = null!;
        public virtual DbSet<FestivalVideo> FestivalVideos { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<OnlineStream> OnlineStreams { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<PageDetail> PageDetails { get; set; } = null!;
        public virtual DbSet<Picture> Pictures { get; set; } = null!;
        public virtual DbSet<ProvinceCode> ProvinceCodes { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Rule> Rules { get; set; } = null!;
        public virtual DbSet<Slider> Sliders { get; set; } = null!;
        public virtual DbSet<TVCover> TVCovers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=185.255.88.106,2019; initial catalog=ArpaWebsite_DB;User Id=Parham;Password=Parham1382;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Arabic_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("City");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany()
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_ProvinceCode");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Cover).IsUnicode(false);

                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GenreId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ManufacturedBy).HasMaxLength(20);

                entity.Property(e => e.Poster).IsUnicode(false);

                entity.Property(e => e.Rating)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.URL).IsUnicode(false);
            });

            modelBuilder.Entity<Festival>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<FestivalVideo>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.NationalCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Festival)
                    .WithMany(p => p.FestivalVideos)
                    .HasForeignKey(d => d.FestivalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FestivalVideos_Festivals");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FestivalVideos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FestivalVideos_Users");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.FullName).HasMaxLength(25);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(13)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(30);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Alias)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cover).IsUnicode(false);

                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GenreId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Language).HasMaxLength(50);

                entity.Property(e => e.ManufacturedBy).HasMaxLength(20);

                entity.Property(e => e.Poster).IsUnicode(false);

                entity.Property(e => e.Rating)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.URL).IsUnicode(false);
            });

            modelBuilder.Entity<OnlineStream>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.Property(e => e.Url).IsUnicode(false);
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PageName).HasMaxLength(30);

                entity.Property(e => e.PageURL)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PageDetail>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PageTitle).HasMaxLength(30);
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("Picture");

                entity.Property(e => e.ImageName).HasMaxLength(250);
            });

            modelBuilder.Entity<ProvinceCode>(entity =>
            {
                entity.ToTable("ProvinceCode");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Province).HasMaxLength(50);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Rule>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SlideImage).IsUnicode(false);

                entity.Property(e => e.URL).IsUnicode(false);
            });

            modelBuilder.Entity<TVCover>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.BirthDate).HasMaxLength(10);

                entity.Property(e => e.DisplayName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('data:image/png;base64,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')");

                entity.Property(e => e.ImageThumb)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('data:image/png;base64,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')");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.NationalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PassWord)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
