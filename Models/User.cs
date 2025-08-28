namespace BlogsAPI.Models
{
    public record UserDTO(string Email, string Password , string Role);
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User"; // e.g., "Admin", "User"

    
    }
}
