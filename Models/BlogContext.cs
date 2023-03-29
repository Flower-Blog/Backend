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

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Flower> Flowers { get; set; }

    public virtual DbSet<FlowerGiver> FlowerGivers { get; set; }

    public virtual DbSet<FlowerOwnerShip> FlowerOwnerShips { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Mail> Mail { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articles__3214EC07794FE530");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Contents)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SubStandard)
                .IsRequired()
                .HasMaxLength(42)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(42)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Articles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Articles__UserId__4AB81AF0");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC07D09F94E6");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Contents)
                .IsRequired()
                .HasMaxLength(42)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Articl__4CA06362");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__UserId__4BAC3F29");
        });

        modelBuilder.Entity<Flower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Flowers__3214EC0780F60561");

            entity.Property(e => e.Language)
                .IsRequired()
                .HasMaxLength(42)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(42)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FlowerGiver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FlowerGi__3214EC071AEA7D30");

            entity.ToTable("FlowerGiver");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Article).WithMany(p => p.FlowerGivers)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerGiv__Artic__5165187F");

            entity.HasOne(d => d.Flower).WithMany(p => p.FlowerGivers)
                .HasForeignKey(d => d.FlowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerGiv__Flowe__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.FlowerGivers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerGiv__UserI__5070F446");
        });

        modelBuilder.Entity<FlowerOwnerShip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FlowerOw__3214EC0757BEB544");

            entity.ToTable("FlowerOwnerShip");

            entity.HasOne(d => d.Flower).WithMany(p => p.FlowerOwnerShips)
                .HasForeignKey(d => d.Flowerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerOwn__Flowe__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.FlowerOwnerShips)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlowerOwn__UserI__52593CB8");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.Comment).WithMany()
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Likes__CommentId__4D94879B");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Likes__UserId__4E88ABD4");
        });

        modelBuilder.Entity<Mail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VerificationCode)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0724A23662");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(42)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(42)
                .IsUnicode(false);
            entity.Property(e => e.Nonce)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
