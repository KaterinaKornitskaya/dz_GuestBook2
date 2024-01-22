using System.ComponentModel.DataAnnotations;

namespace dz_GuestBook2.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }
        public ICollection<Messages>? Messages { get; set; }
    }
}
