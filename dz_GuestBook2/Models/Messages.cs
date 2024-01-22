using System.ComponentModel.DataAnnotations;

namespace dz_GuestBook2.Models
{
    public class Messages
    {
        public int Id { get; set; }

        [Display (Name="Сообщение")]
        public string? Message { get; set; }

        [Display(Name = "Дата")]
        public DateTime MessageDate { get; set; }
        public User? User { get; set; }
    }
}
