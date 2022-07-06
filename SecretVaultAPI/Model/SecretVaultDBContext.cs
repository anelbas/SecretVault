using System;
using System.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace SecretVaultAPI.Model
{
    public partial class SecretVaultDBContext : DbContext
    {
        public SecretVaultDBContext()
        {
        }

        public SecretVaultDBContext(DbContextOptions<SecretVaultDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostGroup> PostGroups { get; set; }
        public virtual DbSet<PrivacyStatus> PrivacyStatuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                string myDbConnection = _configuration.GetConnectionString("DBConnection");
                optionsBuilder.UseSqlServer(myDbConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.GroupName).HasMaxLength(255);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__Group__CreatedBy__4222D4EF");
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.ToTable("GroupUser");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupUser__Group__440B1D61");

                entity.HasOne(d => d.Usr)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.UsrId)
                    .HasConstraintName("FK__GroupUser__UsrId__4316F928");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Content).HasMaxLength(255);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.PrivacyStatus)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PrivacyStatusId)
                    .HasConstraintName("FK__Post__PrivacySta__403A8C7D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Post__UserId__412EB0B6");
            });

            modelBuilder.Entity<PostGroup>(entity =>
            {
                entity.ToTable("PostGroup");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.PostGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__PostGroup__Group__45F365D3");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostGroups)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostGroup__PostI__44FF419A");
            });

            modelBuilder.Entity<PrivacyStatus>(entity =>
            {
                entity.ToTable("PrivacyStatus");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
