namespace BlogsAPI.Services
{
    using BlogsAPI.Data;
    using BlogsAPI.Models;

    public interface IAuthorsService
    {
        public IEnumerable<Author> GetAllAuthors();
        public Task<Author?> GetAuthor(int id);

        public  Task<IEnumerable<Post>> GetPosts(int AuthorId);
        public Author AddAuthor(AddAuthorDTO author);
        public Author UpdateAuthor(int id, UpdateAuthorDTO updateAuthor);
        public void DeleteAuthor(int id);


    }   
    public class AuthorsService : IAuthorsService
    {
        private readonly BlogsDbContext _context;
        public AuthorsService(BlogsDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }
        public async Task<Author?> GetAuthor(int id)
        {
            return await _context.Authors.FindAsync(id);
        }
        public Author AddAuthor(AddAuthorDTO author)
        {
            var newAuthor = new Author(author);
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();
            return newAuthor;
        }
        public Author UpdateAuthor(int id, UpdateAuthorDTO updateAuthor)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                author.Name = updateAuthor.Name;
                author.Bio = updateAuthor.Bio;
                _context.SaveChanges();
                return author;
            }
            throw new KeyNotFoundException($"Author with ID {id} not found.");
        }
        public void DeleteAuthor(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                PostsService Service = new PostsService(_context);
                //Remove all posts & comments associated with the author
                _context.Comments.RemoveRange(_context.Comments.Where(c => c.AuthorId == id));
                // Remove all posts associated with the author
                _context.Posts.Where(p => p.AuthorId == id)
                    .ToList()
                    .ForEach(p => Service.DeletePost(p.Id));
                // Remove the author from the context
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }


        public async Task<IEnumerable<Post>> GetPosts(int AuthorId) {

            return _context.Posts
                .Where(P => P.AuthorId == AuthorId);
        }

    }
}
