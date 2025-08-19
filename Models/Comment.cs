namespace BlogsAPI.Models
{

    /*
     * Comments (Id, Content, CreatedDate, PostId, AuthorId) 
     */

    //Handling DTOs for adding and update
    public record AddCommentDTO(string Content, int AuthorId, int PostId);
    public record UpdateCommentDTO(int Id, string Content);
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        // Navigation properties
        public Post Post { get; set; }
        public Author Author { get; set; }
    }
}
