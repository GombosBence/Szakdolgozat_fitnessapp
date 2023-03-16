using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace azuretest.Models;

public partial class AspWebApiDbContext : DbContext
{
    public AspWebApiDbContext()
    {
    }

    public AspWebApiDbContext(DbContextOptions<AspWebApiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MealsHistory> MealsHistories { get; set; }

    public virtual DbSet<Milestone> Milestones { get; set; }

    public virtual DbSet<StepsHistory> StepsHistories { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    public virtual DbSet<UserMilestone> UserMilestones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:asp-webapidbserver.database.windows.net,1433;Initial Catalog=ASP_WebAPI_db;Persist Security Info=False;User ID=szdadmin;Password=Admin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MealsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Meals_Hi__3213E83F3E9989A7");

            entity.ToTable("Meals_History");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FoodName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Food_Name");
            entity.Property(e => e.QuantityInGrams).HasColumnName("Quantity_inGrams");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.MealsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Meals_His__Quant__628FA481");
        });

        modelBuilder.Entity<Milestone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mileston__3214EC07D190DFCD");

            entity.Property(e => e.MilestoneName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Milestone_Name");
        });

        modelBuilder.Entity<StepsHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Steps_Hi__3213E83FA408B5E2");

            entity.ToTable("Steps_History");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaloriesBurnt).HasColumnName("Calories_Burnt");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.StepsHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Steps_His__Time___656C112C");
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_LoginData");

            entity.ToTable("User_Information");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.CalorieGoal).HasColumnName("Calorie_Goal");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaximumCalStreak).HasColumnName("Maximum_Cal_streak");
            entity.Property(e => e.MaximumSteps).HasColumnName("Maximum_Steps");
            entity.Property(e => e.MilestoneScore).HasColumnName("Milestone_Score");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StepGoal).HasColumnName("Step_Goal");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserMilestone>(entity =>
        {
            entity.ToTable("User_Milestones");

            entity.Property(e => e.MilestoneId).HasColumnName("Milestone_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Milestone).WithMany(p => p.UserMilestones)
                .HasForeignKey(d => d.MilestoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Mile__Score__70DDC3D8");

            entity.HasOne(d => d.User).WithMany(p => p.UserMilestones)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Mile__User___71D1E811");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
