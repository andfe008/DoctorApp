using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entidades
{
    public class Especialidad
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength =1, ErrorMessage ="El Nombre debe ser Minimo 1 Maximo de 60 caracteres")]
        public string NombreEspecialidad { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La Descrición debe ser Minimo 1 Maximo de 100 caracteres")]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
