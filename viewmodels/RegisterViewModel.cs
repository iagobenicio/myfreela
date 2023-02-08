using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace myfreela.viewmodels
{
    public class RegisterViewModel : LoginViewModel
    {   
        public int Id { get; set; }
        
        [Display(Name = "Nome de usuário")]
        [Required(ErrorMessage = "Preencha nome de usuário")]
        public string? UserName { get; set; }

        [Display(Name = "Confirmar senha")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Compare("Password",ErrorMessage = "As senhas não batem")]
        public string? Confpassword { get; set; }
    }
}