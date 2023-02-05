using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myfreela.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceByHours { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime LastStarted { get; set; }
        public DateTime CreatedAt { get; set; }
        public User? user { get; set; }

    }
}