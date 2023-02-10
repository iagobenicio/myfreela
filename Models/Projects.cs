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
        public ProjectStatus Status { get; set; }
        public DateTime? LastStart { get; set; }
        public DateTime? LastPause { get; set; }
        public DateTime CreatedAt { get; set; }
        public User? user { get; set; }

    }
}