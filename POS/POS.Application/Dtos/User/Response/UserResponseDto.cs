using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.User.Response
{
    public class UserResponseDto
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? StateUser { get; set; }      

    }
}
