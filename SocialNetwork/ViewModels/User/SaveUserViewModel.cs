using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el nombre...")]
        public string Nombre { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el apellido...")]
        public string Apellido { get; set; }

        [RegularExpression(@"^(\(809\)|809|\(829\)|829|\(849\)|849)?[2-9][0-9]{2}-[0-9]{4}$",
         ErrorMessage = "El número de teléfono debe ser formato RD")]
        [MaxLength(13)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Escriba el telefono...")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Inserte un archivo")]
        [DataType(DataType.Upload)]
        public IFormFile? file { get; set; }

        public bool Activo { get; set; }
        public string? ImgPath { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Escriba el correo...")]
        public string Correo { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba el username...")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Escriba la contraseña...")]
        public string Contraseña { get; set; }

        [Compare(nameof(Contraseña), ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Escriba la confirmación")]
        public string ConfirmContraseña { get; set;  }
    }
}
