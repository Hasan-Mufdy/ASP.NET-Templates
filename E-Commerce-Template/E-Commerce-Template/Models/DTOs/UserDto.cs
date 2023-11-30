namespace E_Commerce_Template.Models.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public IList<string> Roles { get; set; }
    }
}
