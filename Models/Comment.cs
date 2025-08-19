namespace BlogsAPI.Models
{

    /*
     * Comments (Id, Content, CreatedDate, PostId, AuthorId) 
     */
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
