﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyrmidonAPI.Models;

public partial class MyrmidonContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public MyrmidonContext()
    {
    }

    public MyrmidonContext(DbContextOptions<MyrmidonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Fact> Facts { get; set; }

    public virtual DbSet<JournalEntry> JournalEntries { get; set; }

    public virtual DbSet<Mood> Moods { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<SessionToken> SessionTokens { get; set; }

    public virtual DbSet<Tension> Tensions { get; set; }

    public virtual DbSet<Therapist> Therapists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=myrmidon;user=root;password=mmyrmidontest",
            ServerVersion.Parse("8.0.31-mysql"));*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(x => x.Id);

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PRIMARY");

            entity.ToTable("appointments");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("notes");

            entity.HasMany(d => d.Users).WithMany(p => p.Appointments)
                .UsingEntity<Dictionary<string, object>>(
                    "AppointmentUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("AppointmentUsers_ibfk_2"),
                    l => l.HasOne<Appointment>().WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("AppointmentUsers_ibfk_1"),
                    j =>
                    {
                        j.HasKey("AppointmentId", "Id")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.HasIndex(new[] { "Id" }, "Id");
                    });
        });

        modelBuilder.Entity<Fact>(entity =>
        {
            entity.HasKey(e => e.FactId).HasName("PRIMARY");

            entity.HasIndex(e => e.FactId, "fact_id").IsUnique();

            entity.Property(e => e.Fact1)
                .HasMaxLength(511)
                .HasColumnName("fact");
            entity.Property(e => e.FactId)
                .ValueGeneratedOnAdd()
                .HasColumnName("fact_id");
            entity.Property(e => e.LastShown)
                .HasColumnType("datetime")
                .HasColumnName("last_shown");
        });

        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.HasKey(e => e.JournalEntryId).HasName("PRIMARY");

            entity.ToTable("Journal_entry");

            entity.HasIndex(e => e.Id, "Id");

            entity.Property(e => e.JournalEntryId).HasColumnName("journal_entry_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.JournalEntry1)
                .HasMaxLength(1000)
                .HasColumnName("journal_entry");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.HasOne(d => d.User).WithMany(p => p.JournalEntries)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Journal_entry_ibfk_1");
        });

        modelBuilder.Entity<Mood>(entity =>
        {
            entity.HasKey(e => e.MoodId).HasName("PRIMARY");

            entity.ToTable("Mood");

            entity.HasIndex(e => e.Id, "Id");

            entity.Property(e => e.MoodId).HasColumnName("mood_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.HasOne(d => d.User).WithMany(p => p.Moods)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Mood_ibfk_1");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PRIMARY");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.Id, "Id");

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.MedicalHistory)
                .HasMaxLength(1000)
                .HasColumnName("medical_history");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("Patient_ibfk_1");

            entity.HasMany(d => d.Therapists).WithMany(p => p.Patients)
                .UsingEntity<Dictionary<string, object>>(
                    "PatientTherapist",
                    r => r.HasOne<Therapist>().WithMany()
                        .HasForeignKey("TherapistId")
                        .HasConstraintName("PatientTherapist_ibfk_2"),
                    l => l.HasOne<Patient>().WithMany()
                        .HasForeignKey("PatientId")
                        .HasConstraintName("PatientTherapist_ibfk_1"),
                    j =>
                    {
                        j.HasKey("PatientId", "TherapistId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("PatientTherapist");
                        j.HasIndex(new[] { "TherapistId" }, "therapist_id");
                    });
        });

        modelBuilder.Entity<SessionToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PRIMARY");

            entity.ToTable("session_tokens");

            entity.HasIndex(e => e.TokenId, "token_id").IsUnique();

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.TokenId).HasColumnName("token_id");
            entity.Property(e => e.ExpirationTime)
                .HasColumnType("timestamp")
                .HasColumnName("expiration_time");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.SessionTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("session_tokens_ibfk_1");
        });

        modelBuilder.Entity<Tension>(entity =>
        {
            entity.HasKey(e => e.TensionId).HasName("PRIMARY");

            entity.ToTable("Tension");

            entity.HasIndex(e => e.Id, "Id");

            entity.Property(e => e.TensionId).HasColumnName("tension_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.HasOne(d => d.User).WithMany(p => p.Tensions)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tension_ibfk_1");
        });

        modelBuilder.Entity<Therapist>(entity =>
        {
            entity.HasKey(e => e.TherapistId).HasName("PRIMARY");

            entity.ToTable("Therapist");

            entity.HasIndex(e => e.Id, "Id");

            entity.Property(e => e.TherapistId).HasColumnName("therapist_id");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.HasOne(d => d.User).WithMany(p => p.Therapists)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("Therapist_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.Id, "Id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Address)
                .HasMaxLength(320)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasColumnType("datetime")
                .HasColumnName("birth_date");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(12)
                .HasColumnName("postal_code");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}