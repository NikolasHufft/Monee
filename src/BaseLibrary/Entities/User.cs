using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
