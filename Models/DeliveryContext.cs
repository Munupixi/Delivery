using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Delivery.Models;

public partial class DeliveryContext : DbContext
{
    public DeliveryContext()
    {
    }

    public DeliveryContext(DbContextOptions<DeliveryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserOrder> UserOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;password=123123123;database=Delivery", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.HasIndex(e => e.Title, "Title").IsUnique();

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("Role_Id");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("status");

            entity.HasIndex(e => e.Title, "Title").IsUnique();

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("Status_Id");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Login, "Login").IsUnique();

            entity.HasIndex(e => e.RoleId, "Role_Id");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_ibfk_1");
        });

        modelBuilder.Entity<UserOrder>(entity =>
        {
            entity.HasKey(e => e.UserOrderId).HasName("PRIMARY");

            entity.ToTable("user_order");

            entity.HasIndex(e => e.ExecutorId, "Executor_Id");

            entity.HasIndex(e => e.StatusId, "Status_Id");

            entity.Property(e => e.UserOrderId)
                .ValueGeneratedNever()
                .HasColumnName("User_Order_Id");
            entity.Property(e => e.ClientCompleteName)
                .HasMaxLength(150)
                .HasColumnName("Client_Complete_Name");
            entity.Property(e => e.ClientPhone)
                .HasMaxLength(11)
                .HasColumnName("Client_Phone");
            entity.Property(e => e.ExecutorId).HasColumnName("Executor_Id");
            entity.Property(e => e.ItemTitle)
                .HasMaxLength(100)
                .HasColumnName("Item_Title");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_Date");
            entity.Property(e => e.StatusId).HasColumnName("Status_Id");

            entity.HasOne(d => d.Executor).WithMany(p => p.UserOrders)
                .HasForeignKey(d => d.ExecutorId)
                .HasConstraintName("user_order_ibfk_1");

            entity.HasOne(d => d.Status).WithMany(p => p.UserOrders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("user_order_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
