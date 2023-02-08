using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace myfreela.viewmodels
{
    public class LoginViewModel
    {   

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        public string? Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string? Password { get; set; }
        
    }
}