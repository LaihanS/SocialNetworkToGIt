using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el nombre de usuario...")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Escriba la contraseña...")]
        public string Password { get; set; }
    }
}
