using ORM_MINI_PROJECT.Models.Common;

namespace ORM_MINI_PROJECT.Models
{
    public class User : BaseEntity
    {
        public string Fulname { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
