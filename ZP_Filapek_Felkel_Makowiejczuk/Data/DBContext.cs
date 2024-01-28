using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class DBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }
    public DbSet<Grade> Grades { get; set; }

    public DBContext()
    {

    }

    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ShoolDataBase;User Id=sa;Password=yourStrong(!)Password;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.City).IsRequired();
            entity.Property(e => e.Street).IsRequired();
            entity.Property(e => e.PostalCode).IsRequired();
            entity.Property(e => e.HouseNumber).IsRequired();
            entity.Property(e => e.UserType).IsRequired();
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.TeacherID).IsRequired();
            entity.Property(e => e.Date).IsRequired();
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StudentID).IsRequired();
            entity.Property(e => e.LessonID).IsRequired();
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StudentID).IsRequired();
            entity.Property(e => e.TeacherID).IsRequired();
            entity.Property(e => e.LessonID).IsRequired();
            entity.Property(e => e.GradeValue).IsRequired();
            entity.Property(e => e.GradeDate).IsRequired();
        });

        //    modelBuilder.Entity<Grade>()
        //.HasOne(g => g.LessonID)
        //.WithMany()
        //.HasForeignKey(g => g.LessonID);
        //modelBuilder.Entity<Grade>().HasMany(x => x.LessonID).WithOne(x=>x.Grade).HasForeignKey(x=>x.GradeID);
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany()
            .HasForeignKey(g => g.StudentID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Teacher)
            .WithMany()
            .HasForeignKey(g => g.TeacherID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Lesson)
            .WithMany()
            .HasForeignKey(g => g.LessonID);

        modelBuilder.Entity<Lesson>()
            .HasOne(g => g.Teacher)
            .WithMany()
            .HasForeignKey(g => g.TeacherID);

        modelBuilder.Entity<StudentClass>()
            .HasOne(g => g.Student)
            .WithMany()
            .HasForeignKey(g => g.StudentID)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<StudentClass>()
            .HasOne(g => g.Lesson)
            .WithMany()
            .HasForeignKey(g => g.LessonID)
            .OnDelete(DeleteBehavior.Restrict);


        //    modelBuilder.Entity<StudentClass>()
        //.HasOne(sc => sc.Lesson)
        //.WithMany(l => l.Teacher)
        //.HasForeignKey(sc => sc.NewLessonID);
    }

}
