using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UseGroup.DataModel.Models
{
    public partial class PersonGroupsContext : DbContext
    {

        public PersonGroupsContext(DbContextOptions<PersonGroupsContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Person> Person { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PersonGroups;Integrated Security=True;ConnectRetryCount= 0");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Name);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_Group");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
