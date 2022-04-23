
using ParcialTickets.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parcial2.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Display(Name = "¿Está en uso?")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool WasUsed { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Display(Name = "Documento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public String Document { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public String Name { get; set; }

        [Display(Name = "Entrada")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Entrance Entrance { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "La {0} es obligatorio")]
        public DateTime Date { get; set; }

        public int EntranceId { get; set; }


    }
}
