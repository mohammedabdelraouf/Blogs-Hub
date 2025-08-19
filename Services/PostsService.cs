using BlogsAPI.Data;
using BlogsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogsAPI.Services
{

    public interface IPostsService
    {
        // Define methods that the PostsService should implement
        IEnumerable<Post> GetAllPosts();
        Task<Post?> GetPost(int id);
        Post AddPost(AddpostDTO post);
        Post UpdatePost(int id, UpdatePostDTO newPost);
        void DeletePost(int id);
    }
    public class PostsService : IPostsService
    {
        private readonly BlogsDbContext  _context ;

        public PostsService(BlogsDbContext context)
        {
            _context = context;
        }
        public  IEnumerable<Post> GetAllPosts()
        {
            try
            {
                return  _context.Posts.ToList();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw new Exception("An error occurred while retrieving all posts.", ex);
            }
        }

        public  async Task<Post?> GetPost(int id)
        {
            try
            {
                var post = await  _context.Posts.FindAsync(id);

                return post;

            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw new Exception($"An error occurred while retrieving the post with ID {id}.", ex);
            }
        }
        public Post AddPost(AddpostDTO PostDTO)
        {
            try
            {
                if (PostDTO == null)
                    throw new ArgumentNullException(nameof(PostDTO));
                var post = new Post(PostDTO);
                _context.Posts.Add(post);
                _context.SaveChanges();
                return  post;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw new Exception("An error occurred while adding the post.", ex);
            }
          
        }


        public  Post UpdatePost(int id, UpdatePostDTO newPost)
        {
            try
            {
                var post =  _context.Posts.Find(id);
                if (post == null)
                    throw new KeyNotFoundException($"Post with ID {id} not found.");
                post.Title = newPost.Title;
                post.Content = newPost.Content;
                post.UpdatedDate = DateTime.Now; // Update the timestamp
                 _context.SaveChangesAsync();
                return post;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw new Exception($"An error occurred while updating the post with ID {id}.", ex);
            }
        }

        public void DeletePost(int id)
        {
            try
            {
                var post = _context.Posts.Find(id);
                if (post == null)
                    throw new KeyNotFoundException($"Post with ID {id} not found.");

                _context.Comments.Where(c => c.PostId == id)
                    .ToList()
                    .ForEach(c => _context.Comments.Remove(c)); // Remove related comments
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw new Exception("An error occurred while deleting the post.", ex);
            }
        }
    }
}
