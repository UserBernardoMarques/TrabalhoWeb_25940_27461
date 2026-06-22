using System;
using System.ComponentModel.DataAnnotations;

namespace TrabalhoWeb_25940.Models
{
    public class Inscricao
    {
        [Key]
        public int Id { get; set; }

        // Liga ao Participante que pediu para ir
        public int ParticipanteId { get; set; }
        public Participante? Participante { get; set; }

        // Liga ao Workshop que ele escolheu
        public int WorkshopId { get; set; }
        public Workshop? Workshop { get; set; }

        [Display(Name = "Data do Pedido")]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Display(Name = "Estado")]
        public bool Aprovada { get; set; } = false; // Admin precisa de aprovar!
    }
}