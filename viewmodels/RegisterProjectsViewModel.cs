using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace myfreela.viewmodels
{
    public class RegisterProjectsViewModel
    {    
        [Display(Name = "Nome do projeto")]
        [Required]
        public string? Name { get; set; }

        [Display(Name = "Preço por hora")]
        [Required]
        public decimal PriceByHours { get; set; }
    }
}