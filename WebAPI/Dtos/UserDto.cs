using System.Text.Json.Serialization;

namespace WebAPI.Dtos
{
    public class UserDto
    {

        [JsonIgnore]
        public int Idusers { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
