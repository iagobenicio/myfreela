using System.ComponentModel.DataAnnotations;

namespace myfreela.viewmodels
{
    public class RegisterProjectsViewModel
    {    

        public int Id { get; set; }

        [Display(Name = "Nome do projeto")]
        [Required]
        public string? Name { get; set; }

        [Display(Name = "Preço por hora")]
        [Required]
        public decimal PriceByHours { get; set; }
    }
}