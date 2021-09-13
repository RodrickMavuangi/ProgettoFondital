using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Resources
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Token { get; set; }
    }
}
