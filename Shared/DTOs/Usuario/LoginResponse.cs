using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Usuario
{
    public class LoginResponse
    {
        public string? UserUid { get; set; }
        public string Error { get; set; }
    }
}
