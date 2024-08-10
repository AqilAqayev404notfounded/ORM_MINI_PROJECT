using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Fulname { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
    }
}
