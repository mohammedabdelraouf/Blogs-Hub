using BlogsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogsAPI.Data
{
    public class BlogsDbContext : DbContext
    {
        public BlogsDbContext(DbContextOptions<BlogsDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Post entity
            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Post>()
                .Property(p => p.Content)
                .IsRequired();
            modelBuilder.Entity<Post>()
                .Property(p => p.CreatedDate)
                .IsRequired();
            modelBuilder.Entity<Post>()
                .Property(p => p.UpdatedDate)
                .IsRequired();

            // Configure the Author entity
            modelBuilder.Entity<Author>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Author>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Author>()
                .Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Author>()
                .Property(a => a.Bio)
                .HasMaxLength(500);
            modelBuilder.Entity<Author>()
                .Property(a => a.JoinDate)
                .IsRequired();

            // Configure the Comment entity
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Comment>()
                .Property(c => c.Content)
                .IsRequired();
            modelBuilder.Entity<Comment>()
                .Property(c => c.CreatedDate)
                .IsRequired();


            // Relationships


            // Author–Post: If an Author is deleted, their Posts are also deleted
            modelBuilder.Entity<Post>()
                    .HasOne(p => p.Author)
                    .WithMany(a => a.Posts)
                    .HasForeignKey(p => p.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

            // Author–Comment: If an Author is deleted, their Comments are also deleted
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Post–Comment: If a Post is deleted, its Comments are also deleted
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Ahmed Youssef", Email = "ahmed@example.com", Bio = "Tech enthusiast and blogger.", JoinDate = new DateTime(2023, 1, 15) },
                new Author { Id = 2, Name = "Sara El-Masry", Email = "sara@example.com", Bio = "Writes about lifestyle.", JoinDate = new DateTime(2023, 3, 22) }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "Getting Started with Docker", Content = "A beginner's guide to Docker.", CreatedDate = new DateTime(2023, 5, 1), UpdatedDate = new DateTime(2023, 5, 2), AuthorId = 1 },
                new Post { Id = 2, Title = "Healthy Habits", Content = "Tips for a balanced lifestyle.", CreatedDate = new DateTime(2023, 6, 10), UpdatedDate = new DateTime(2023, 6, 12), AuthorId = 2 }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, Content = "Great article, very helpful!", CreatedDate = new DateTime(2023, 5, 3), PostId = 1, AuthorId = 2 },
                new Comment { Id = 2, Content = "I love these tips, thanks!", CreatedDate = new DateTime(2023, 6, 13), PostId = 2, AuthorId = 1 }
            );


        }



    }
}
