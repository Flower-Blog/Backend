using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotnetWebApi.Models;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; } = null!;

    public virtual DbSet<Comment> Comments { get; set; } = null!;

    public virtual DbSet<Flower> Flowers { get; set; } = null!;

    public virtual DbSet<FlowerGiver> FlowerGivers { get; set; } = null!;

    public virtual DbSet<FlowerOwnership> FlowerOwnerships { get; set; } = null!;

    public virtual DbSet<Level> Levels { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articles__3213E83F12483DC5");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Contents)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contents");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Title)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Articles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Articles__userId__17F790F9");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3213E83F1A465764");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ArticleId).HasColumnName("articleId");
            entity.Property(e => e.Contents)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("contents");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__articl__19DFD96B");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__userId__18EBB532");
        });

        modelBuilder.Entity<Flower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Flowers__3213E83FC6CE96DA");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Language)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("language");
            entity.Property(e => e.Name)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FlowerGiver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FlowerGi__3213E83F8A3AAB87");

            entity.ToTable("FlowerGiver");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Flowerid).HasColumnName("flowerid");
            entity.Property(e => e.TargetId).HasColumnName("targetId");
            entity.Property(e => e.TargetType)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("targetType");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Flower).WithMany(p => p.FlowerGivers)
                .HasForeignKey(d => d.Flowerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerGiv__flowe__1AD3FDA4");

            entity.HasOne(d => d.User).WithMany(p => p.FlowerGivers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerGiv__userI__1BC821DD");
        });

        modelBuilder.Entity<FlowerOwnership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FlowerOw__3213E83F9B57B1EF");

            entity.ToTable("FlowerOwnership");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FlowerCount).HasColumnName("flowerCount");
            entity.Property(e => e.Flowerid).HasColumnName("flowerid");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Flower).WithMany(p => p.FlowerOwnerships)
                .HasForeignKey(d => d.Flowerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerOwn__flowe__1DB06A4F");

            entity.HasOne(d => d.User).WithMany(p => p.FlowerOwnerships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerOwn__userI__1CBC4616");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Level__3213E83F55806AD1");

            entity.ToTable("Level");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F3FFE13B2");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("account");
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.LevelId).HasColumnName("levelId");
            entity.Property(e => e.Name)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Level).WithMany(p => p.Users)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__levelId__17036CC0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
