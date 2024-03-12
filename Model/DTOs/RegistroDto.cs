using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "Usuario requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Contraseña Requerida")]
        [StringLength(10, MinimumLength = 4 ,ErrorMessage ="El password debe ser minimo de 4 caracteres y un maximo de 10")]
        public string Password { get; set; }



    }
}
