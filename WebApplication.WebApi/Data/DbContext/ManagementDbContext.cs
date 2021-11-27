using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.WebApi.Data.Entity;

namespace WebApplication.WebApi.Data.DbContext
{
    public class ManagementDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Class> Classes { set; get; }
        public DbSet<Topic> Topics { set; get; }
        public DbSet<UserCourse> UserCourses { set; get; }
        public DbSet<Course> Courses { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<UserClass> UserClasses { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Class>(x =>
            {
                x.ToTable("Classes");
                x.HasKey(x => x.Id);
            });
            // Topic of Dat
            builder.Entity<Topic>(x =>
            {
                x.ToTable("Topics");
                x.HasKey(x => x.Id);
                x.HasOne(x => x.Course).WithMany(x => x.Topics).HasForeignKey(x => x.CourseId);
            });
            // Category of Minh
            builder.Entity<Category>(x =>
            {
                x.ToTable("Categories");
                x.HasKey(x => x.Id);
            });
            // Course of Lap
            builder.Entity<Course>(x =>
            {
                x.ToTable("Courses");
                x.HasKey(x => x.Id);
            });
            builder.Entity<CourseCategory>(x =>
            {
                x.ToTable("CourseCategories");
                x.HasKey(x => new { x.CategoryId, x.CourseId });
                x.HasOne(x => x.Category).WithMany(x => x.CourseCategories).HasForeignKey(x => x.CategoryId);
                x.HasOne(x => x.Course).WithMany(x => x.CourseCategories).HasForeignKey(x => x.CourseId);
            });
            builder.Entity<UserCourse>(x =>
            {
                x.ToTable("UserCourses");
                x.HasKey(x => new { x.UserId, x.CourseId });
                x.HasOne(x => x.Course).WithMany(x => x.UserCourses).HasForeignKey(x => x.CourseId);
                x.HasOne(x => x.AppUser).WithMany(x => x.UserCourses).HasForeignKey(x => x.UserId);
            });
            builder.Entity<UserClass>(x =>
            {
                x.ToTable("UserClasses");
                x.HasKey(x => new { x.UserId, x.ClassId });
                x.HasOne(x => x.Class).WithMany(x => x.UserClasses).HasForeignKey(x => x.ClassId);
                x.HasOne(x => x.AppUser).WithMany(x => x.UserClasses).HasForeignKey(x => x.UserId);
            });
            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "huynabhaf190133@fpt.edu.vn",
                NormalizedEmail = "huynabhaf190133@fpt.edu.vn",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "1"),
                SecurityStamp = string.Empty,
                FullName = "Nguyen Anh Huy",
                PhoneNumber = "0399056507",
            });
            //IdentityUserLogin
            builder.Entity<IdentityUserLogin<Guid>>().HasKey(x => x.UserId);
            //IdentityUserRole
            builder.Entity<IdentityUserRole<Guid>>().HasKey(x => new { x.UserId, x.RoleId });
            //IdentityUserToken
            builder.Entity<IdentityUserToken<Guid>>().HasKey(x => x.UserId);
        }
    }
}