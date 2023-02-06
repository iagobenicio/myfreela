using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myfreela.context;

namespace myfreela.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        
        private readonly MyFreelaContext _context;
        public UserController(MyFreelaContext context)
        {
            _context = context;
        }
        
        [HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }

    }
}