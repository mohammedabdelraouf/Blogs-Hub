namespace BlogsAPI.Models
{
    /*
     *  Posts (Id, Title, Content, CreatedDate, UpdatedDate, AuthorId)
     */

    public record AddpostDTO(string Title , string Content, int AuthorId);
    public record UpdatePostDTO( int Id ,string Title , string Content);
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int AuthorId { get; set; }
        // Navigation property
        public Author Author { get; set; }
        // Collection of comments related to the post
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // Default constructor
        public Post() { 
        }

        // Constructor to initialize a Post with an AddpostDTO
        public Post(AddpostDTO post) {
           
            this.Content = post.Content;
            this.AuthorId = post.AuthorId;
            this.Title = post.Title;
            this.UpdatedDate= DateTime.Now;
            this.CreatedDate= DateTime.Now;
        }

    }
}
