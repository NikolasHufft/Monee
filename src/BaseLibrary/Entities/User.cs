using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class User : BaseEntity
    {
        [Required, DataType(DataType.EmailAddress), MinLength(10), MaxLength(100)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), MinLength(5), MaxLength(100)]
        public string Password { get; set; }
    }
}
