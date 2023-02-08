using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace myfreela.viewmodels
{
    public class LoginViewModel
    {   

        [Display(Name = "Nome de usuário")]
        [Required(ErrorMessage = "Preencha nome de usuário")]
        public string? UserName { get; set; }
        
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string? Password { get; set; }
        
    }
}