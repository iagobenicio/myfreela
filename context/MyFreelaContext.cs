using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myfreela.Models;

namespace myfreela.context
{
    public class MyFreelaContext : IdentityDbContext<User,IdentityRole<int>, int>
    {
        
        public MyFreelaContext(DbContextOptions<MyFreelaContext> options ) : base (options)
        {

        }

        public DbSet<Projects>? Projects { get; set; }
    }
}