using System.ComponentModel.DataAnnotations;

namespace myfreela.viewmodels
{
    public class RegisterViewModel : LoginViewModel
    {   
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        public string? Email { get; set; }

        [Display(Name = "Confirmar senha")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Compare("Password",ErrorMessage = "As senhas não batem")]
        public string? Confpassword { get; set; }
    }
}