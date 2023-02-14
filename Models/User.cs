using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace myfreela.Models
{
    public class User : IdentityUser<int>
    {
        public List<Projects> Projects { get; } = new List<Projects>();
        
    }
}