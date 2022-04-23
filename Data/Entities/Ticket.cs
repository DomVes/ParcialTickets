using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ParcialTickets.Data.Entities
{
    public class Ticket
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

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "La {0} es obligatorio")]
        public DateTime Date { get; set; }

        [Display(Name = "Entrada")]
        public Entrance Entrada { get; set; }

        public ICollection<Entrance> Entrances { get; set; }

        [JsonIgnore]
        public Entrance Entrance { set; get; }

    }
}
