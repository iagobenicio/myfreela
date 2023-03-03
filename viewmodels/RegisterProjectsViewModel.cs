using System.ComponentModel.DataAnnotations;

namespace myfreela.viewmodels
{
    public class RegisterProjectsViewModel
    {    

        public int Id { get; set; }

        [Display(Name = "Nome do projeto")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string? Name { get; set; }

        [Display(Name = "Preço por hora")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal PriceByHours { get; set; }
    }
}