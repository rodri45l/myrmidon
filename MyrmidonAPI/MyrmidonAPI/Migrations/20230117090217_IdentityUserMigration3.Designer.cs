// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyrmidonAPI.Models;

#nullable disable

namespace MyrmidonAPI.Migrations
{
    [DbContext(typeof(MyrmidonContext))]
    [Migration("20230117090217_IdentityUserMigration3")]
    partial class IdentityUserMigration3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("AppointmentUser", b =>
                {
                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("AppointmentId", "Id")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "Id" }, "Id");

                    b.ToTable("AppointmentUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("appointment_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("notes");

                    b.HasKey("AppointmentId")
                        .HasName("PRIMARY");

                    b.ToTable("appointments", (string)null);
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Fact", b =>
                {
                    b.Property<int>("FactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("fact_id");

                    b.Property<string>("Fact1")
                        .IsRequired()
                        .HasMaxLength(511)
                        .HasColumnType("varchar(511)")
                        .HasColumnName("fact");

                    b.Property<DateTime?>("LastShown")
                        .HasColumnType("datetime")
                        .HasColumnName("last_shown");

                    b.HasKey("FactId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "FactId" }, "fact_id")
                        .IsUnique();

                    b.ToTable("Facts");
                });

            modelBuilder.Entity("MyrmidonAPI.Models.JournalEntry", b =>
                {
                    b.Property<int>("JournalEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("journal_entry_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<string>("JournalEntry1")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("journal_entry");

                    b.HasKey("JournalEntryId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "Id")
                        .HasDatabaseName("Id1");

                    b.ToTable("Journal_entry", (string)null);
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Mood", b =>
                {
                    b.Property<int>("MoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("mood_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.HasKey("MoodId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "Id")
                        .HasDatabaseName("Id2");

                    b.ToTable("Mood", (string)null);
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Patient", b =>
                {
                    b.Property<Guid>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("patient_id");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<string>("MedicalHistory")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("medical_history");

                    b.HasKey("PatientId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "Id")
                        .HasDatabaseName("Id3");

                    b.ToTable("Patient", (string)null);
                });

            modelBuilder.Entity("MyrmidonAPI.Models.SessionToken", b =>
                {
                    b.Property<Guid>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("token_id");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("timestamp")
                        .HasColumnName("expiration_time");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("ip_address");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("userId");

                    b.HasKey("TokenId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "TokenId" }, "token_id")
                        .IsUnique();

                    b.HasIndex(new[] { "UserId" }, "userId");

                    b.ToTable("session_tokens", (string)null);
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Tension", b =>
                {
                    b.Property<int>("TensionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("tension_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.HasKey("TensionId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "Id")
                        .HasDatabaseName("Id4");

                    b.ToTable("Tension", (string)null);
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Therapist", b =>
                {
                    b.Property<Guid>("TherapistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("therapist_id");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.HasKey("TherapistId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "Id")
                        .HasDatabaseName("Id5");

                    b.ToTable("Therapist", (string)null);
                });

            modelBuilder.Entity("MyrmidonAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320)")
                        .HasColumnName("address");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime")
                        .HasColumnName("birth_date");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Gender")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("gender");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("postal_code");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("Sex")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("sex");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("surname");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex(new[] { "Id" }, "Id")
                        .IsUnique()
                        .HasDatabaseName("Id6");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("PatientTherapist", b =>
                {
                    b.Property<Guid>("PatientId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TherapistId")
                        .HasColumnType("char(36)");

                    b.HasKey("PatientId", "TherapistId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "TherapistId" }, "therapist_id");

                    b.ToTable("PatientTherapist", (string)null);
                });

            modelBuilder.Entity("AppointmentUser", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.Appointment", null)
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .IsRequired()
                        .HasConstraintName("AppointmentUsers_ibfk_1");

                    b.HasOne("MyrmidonAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("Id")
                        .IsRequired()
                        .HasConstraintName("AppointmentUsers_ibfk_2");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyrmidonAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyrmidonAPI.Models.JournalEntry", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", "User")
                        .WithMany("JournalEntries")
                        .HasForeignKey("Id")
                        .IsRequired()
                        .HasConstraintName("Journal_entry_ibfk_1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Mood", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", "User")
                        .WithMany("Moods")
                        .HasForeignKey("Id")
                        .IsRequired()
                        .HasConstraintName("Mood_ibfk_1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Patient", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", "User")
                        .WithMany("Patients")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Patient_ibfk_1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyrmidonAPI.Models.SessionToken", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", "User")
                        .WithMany("SessionTokens")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("session_tokens_ibfk_1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Tension", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", "User")
                        .WithMany("Tensions")
                        .HasForeignKey("Id")
                        .IsRequired()
                        .HasConstraintName("Tension_ibfk_1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyrmidonAPI.Models.Therapist", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.User", "User")
                        .WithMany("Therapists")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Therapist_ibfk_1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PatientTherapist", b =>
                {
                    b.HasOne("MyrmidonAPI.Models.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("PatientTherapist_ibfk_1");

                    b.HasOne("MyrmidonAPI.Models.Therapist", null)
                        .WithMany()
                        .HasForeignKey("TherapistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("PatientTherapist_ibfk_2");
                });

            modelBuilder.Entity("MyrmidonAPI.Models.User", b =>
                {
                    b.Navigation("JournalEntries");

                    b.Navigation("Moods");

                    b.Navigation("Patients");

                    b.Navigation("SessionTokens");

                    b.Navigation("Tensions");

                    b.Navigation("Therapists");
                });
#pragma warning restore 612, 618
        }
    }
}
