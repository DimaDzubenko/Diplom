using Diplom.Models.DataModel.Blog;
using Diplom.Models.DataModel.Identity;
using Diplom.Models.DataModel.Shop.Penties;
using Diplom.Models.DataModel.ShopCart;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Blog
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        // Shop       
        // Shop / Penties
        public DbSet<PCategory> PCategories { get; set; }
        public DbSet<PColor> PColors { get; set; }
        public DbSet<Pentie> Penties { get; set; }
        public DbSet<PSize> PSizes { get; set; }
        public DbSet<PBrand> PBrands { get; set; }
        public DbSet<PentieColor> PentieColors { get; set; }
        public DbSet<PentieSize> PentieSizes { get; set; }
        // shop cart
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<PostTag>().
                HasOne(pt => pt.Post).WithMany(pt => pt.PostTags).HasForeignKey(p => p.PostId);

            modelBuilder.Entity<PostTag>().
                HasOne(pt => pt.Tag).WithMany(pt => pt.PostTags).HasForeignKey(p => p.TagId);

            //modelBuilder.Entity<Post>().ToTable("Post");
            //modelBuilder.Entity<Tag>().ToTable("Tag");
            //modelBuilder.Entity<PostTag>().ToTable("PostTag");
            //modelBuilder.Entity<PBrand>().ToTable("PBrand");
            //modelBuilder.Entity<PColor>().ToTable("PColor");
            //modelBuilder.Entity<Pentie>().ToTable("Pentie");
            //modelBuilder.Entity<PCategory>().ToTable("PCategory");
            //modelBuilder.Entity<PSize>().ToTable("PSize");
            //modelBuilder.Entity<PentieColor>().ToTable("PentieColor");
            //modelBuilder.Entity<PentieSize>().ToTable("PentieSize");


            modelBuilder.Entity<PentieColor>()
               .HasKey(c => new { c.PentieId, c.PColorId });

            modelBuilder.Entity<PentieColor>().
                HasOne(pc => pc.Pentie).WithMany(pc => pc.PentiColors).HasForeignKey(p => p.PentieId);

            modelBuilder.Entity<PentieColor>().
                HasOne(pc => pc.PColor).WithMany(pc => pc.PentiColors).HasForeignKey(p => p.PColorId);

            modelBuilder.Entity<PentieSize>()
               .HasKey(c => new { c.PentieId, c.PSizeId });

            modelBuilder.Entity<PentieSize>().
                HasOne(ps => ps.Pentie).WithMany(ps => ps.PentieSizes).HasForeignKey(p => p.PentieId);

            modelBuilder.Entity<PentieSize>().
                HasOne(ps => ps.PSize).WithMany(ps => ps.PentieSizes).HasForeignKey(p => p.PSizeId);

            // Решение проблемы:
            // The entity type 'IdentityUserLogin<string>' requires a primary key to be defined. If you intended to use a keyless entity type call 'HasNoKey()'.
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<IdentityUserLogin<string>>();
            //modelBuilder.Ignore<IdentityUserRole<string>>();
            //modelBuilder.Ignore<IdentityUserClaim<string>>();
            //modelBuilder.Ignore<IdentityUserToken<string>>();
            //modelBuilder.Ignore<IdentityUser<string>>();
            //modelBuilder.Ignore<User>();
        }
    }
}
