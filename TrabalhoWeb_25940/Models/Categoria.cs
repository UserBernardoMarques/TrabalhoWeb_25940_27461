using System.ComponentModel.DataAnnotations;

namespace TrabalhoWeb_25940.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [Display(Name = "Nome da Categoria")]
        public string? Nome { get; set; } // Ex: Programação, Design

        // Relação 1 para Muitos: Uma Categoria tem muitos Workshops
        public List<Workshop> Workshops { get; set; } = new();
    }
}