using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ParcialTickets.Data.Entities
{
    public class Entrance
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Display(Name ="Entrada")]
        public String Description { get; set; }


         public ICollection<Ticket> Tickets { get; set; }

        [Display(Name ="Boletos / Entradas")]
        public int TicketsNumber => Tickets == null ? 0 : Tickets.Count; 


        
        
    }
}
