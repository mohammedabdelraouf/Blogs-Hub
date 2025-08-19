namespace BlogsAPI.Models
{
    /*
     * Authors (Id, Name, Email, Bio, JoinDate)– 
     */

    public record AuthorDTO(string Name, string Email, string Bio);
    public record UpdateAuthorDTO (string Name , string Bio);
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime JoinDate { get; set; }

        // Navigation property for posts
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        // Navigation property for comments     
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();


        // Default constructor
        public Author()
        {
        }
        // Constructor to initialize an Author with an AuthorDTO
        public Author(AuthorDTO author)
        {
            this.Name = author.Name;
            this.Email = author.Email;
            this.Bio = author.Bio;
            this.JoinDate = DateTime.Now; // Set the join date to the current date
        }
    }
}

